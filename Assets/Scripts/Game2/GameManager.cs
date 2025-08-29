using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float saveInterval = 30f; // auto-save mỗi 30s
    private float timer = 0f;

    public GameObject Character;
    public GameObject HighlightController;

    [SerializeField] private SkillConfigSO skillConfigSO;
    [SerializeField] private ItemConfigSO itemConfigSO;
    [SerializeField] private CharacterConfigSO chacterCfgItemSO;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("GameManaer");
        Instance = this;

        skillConfigSO.LoadData();
        itemConfigSO.LoadData();

        if(CharacterConfig.GetInstance.InitData() == null)
        {
            CharacterConfig.GetInstance.ExportToJson(chacterCfgItemSO.datas, typeof(ChacterCfgItem).Name);
        }
       
        Character.GetComponent<CharacterUnit>().data = CharacterConfig.GetInstance.InitData();

    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0; // Tắt VSync để FPS thực sự giới hạn bởi targetFrameRate

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= saveInterval)
        {
            timer = 0f;
            SaveAsync();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveAsync();
    }

    private void OnApplicationQuit()
    {
        SaveAsync();
    }

    private async void SaveAsync()
    {
        if (CharacterConfig.GetInstance)
        await CharacterConfig.GetInstance.SaveJsonAsync();
    }
}