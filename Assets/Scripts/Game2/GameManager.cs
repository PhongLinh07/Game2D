using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float saveInterval = 30f; // auto-save mỗi 30s
    private float lastSave = 0f;

    public GameObject HighlightController;

    private void Awake()
    {
        Instance = this;
        lastSave = saveInterval;
    }

    void Update()
    {
        if (Time.time >= saveInterval + lastSave)
        {
            lastSave = Time.time;
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
        if (ItemUserConfig.GetInstance) await ItemUserConfig.GetInstance.SaveJsonAsync();
        if (CharacterConfig.GetInstance) await CharacterConfig.GetInstance.SaveJsonAsync();
        if (UUIDConfig.GetInstance) await UUIDConfig.GetInstance.SaveJsonAsync();
    }
}