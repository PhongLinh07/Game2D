using System;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    public static Bootstrapper Instance;

    [SerializeField] private SkillConfigSO skillConfigSO;
    [SerializeField] private ItemConfigSO itemConfigSO;
    [SerializeField] private CharacterConfigSO charConfigSO;


    [SerializeField] private GameObject characterPrefab;

    public Action<LogicCharacter> eventWhenCloneCharacter;


    void Awake()
    {
        Instance = this;

        // Đảm bảo Bootstrapper không bị hủy khi load scene mới
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        InitData();
    } 
    
    public void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0; // Tắt VSync để FPS thực sự giới hạn bởi targetFrameRate

        SceneManager.LoadScene("GamePlay");
    }

    public void InitData()
    {
        skillConfigSO.LoadData();
        itemConfigSO.LoadData();

        if (CharacterConfig.GetInstance.InitData() == null)
        {
            Debug.Log("Created ChacterCfgItem!");
            CharacterConfig.GetInstance.ExportToJson(charConfigSO.datas, typeof(CharacterCfgItem).Name);
            CharacterConfig.GetInstance.InitData();
        }
        else
        {
            Debug.Log("Loaded ChacterCfgItem!");
            
        }

    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlay")
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            var player = Instantiate(characterPrefab, CharacterConfig.GetInstance.GetConfigItem(0).position, Quaternion.identity);

            eventWhenCloneCharacter?.Invoke(player.GetComponent<LogicCharacter>());
          
        }
    }



}


