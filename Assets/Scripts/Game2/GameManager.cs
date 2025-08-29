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
    public ChacterCfgItem check;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        skillConfigSO.LoadData();
        itemConfigSO.LoadData();

        CharacterConfig.GetInstance.InitData();
        check = CharacterConfig.GetInstance.GetConfigItem(0);

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