using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SkillConfig gameSkill;
    [SerializeField] private ItemConfig gameItem;
    [SerializeField] private SkillConfigSO skillConfigSO;
    [SerializeField] private SpriteConfigSO spriteConfigSO;
    [SerializeField] private ItemConfigSO itemConfigSO;


    public GameObject Character;
    public GameObject HighlightController;

    public PlayerData R_PlayerData;
   

    

    // Start is called before the first frame update
    void Awake()
    {
        
        Instance = this;
  

        // Load data khi bắt đầu game
        R_PlayerData = SaveSystem.Load<PlayerData>("player");
        Debug.Log("Start Game with: " + R_PlayerData.name);

        SpriteConfig.GetInstance.InitData(spriteConfigSO.ToDict());
        SkillConfig.GetInstance.InitData(skillConfigSO.ToDict());
        ItemConfig.GetInstance.InitData(itemConfigSO.ToDict());
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0; // Tắt VSync để FPS thực sự giới hạn bởi targetFrameRate

    }
}