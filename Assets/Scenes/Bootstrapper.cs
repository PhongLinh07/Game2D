using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    public static Bootstrapper Instance;

    [SerializeField] private CharacterConfigSO charConfigSO;
    [SerializeField] private ItemUserConfigSO itemUserConfigSO;


    public Action<LogicCharacter> eventWhenCloneCharacter;

    private AssetManager assetManager = new();

    void Awake()
    {
        Instance = this;

        // Đảm bảo Bootstrapper không bị hủy khi load scene mới
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        InitData();
    }

    public void InitData()
    {
        assetManager.LoadAsset<SkillConfigSO>(EAsset.SkillConfigSO, (data) => { data.LoadData(); });
        assetManager.LoadAsset<ItemConfigSO>(EAsset.ItemConfigSO, (data) => { data.LoadData();});


        if (ItemUserConfig.GetInstance.InitData() == null)
        {
            Debug.Log($"Created {typeof(ItemUserCfgItem).Name}!");
            ItemUserConfig.GetInstance.ExportToJson(itemUserConfigSO.datas, typeof(ItemUserCfgItem).Name);
            ItemUserConfig.GetInstance.InitData();
        }
        else
        {
            Debug.Log("Loaded ItemUserCfgItem!");

        }

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

    public void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0; // Tắt VSync để FPS thực sự giới hạn bởi targetFrameRate

        SceneManager.LoadScene("GamePlay");
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlay")
        {
            assetManager.LoadAsset<GameObject>(CharacterConfig.GetInstance.GetConfigItem(0).idPrefab, SpawnPlayer);
        }  
    }

    private void SpawnPlayer(GameObject prefab)
    {

        if (GameObject.FindWithTag("Player") == null)
        {
            var player = Instantiate(prefab, CharacterConfig.GetInstance.GetConfigItem(0).position, Quaternion.identity);

            eventWhenCloneCharacter?.Invoke(player.GetComponent<LogicCharacter>());

            Debug.Log("Create character Succeeded!");

            assetManager.ReleaseAsset(CharacterConfig.GetInstance.GetConfigItem(0).idPrefab);

        }
    }
}


