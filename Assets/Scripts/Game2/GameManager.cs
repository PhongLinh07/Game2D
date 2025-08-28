using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public GameObject Character;
    public GameObject HighlightController;

    public ChacterCfgItem check;
    // Start is called before the first frame update
    void Awake()
    {
        
        Instance = this;


        // Load data khi bắt đầu game
        //SaveSystem.Load<CharacterUnitSO>("player").ToCharacterUnit(R_CharacterUnit);
        // Debug.Log("Start Game with: " + R_CharacterUnit.infomation.name);

        ConfigMgr<ChacterCfgItem>.GetInstance.InitData();
        check = ConfigMgr<ChacterCfgItem>.GetInstance.GetConfigItem(0);
        SpriteConfig.GetInstance.InitData();
        ConfigMgr<SkillCfgItem>.GetInstance.InitData();
        ConfigMgr<ItemCfgItem>.GetInstance.InitData();

    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0; // Tắt VSync để FPS thực sự giới hạn bởi targetFrameRate

    }
}