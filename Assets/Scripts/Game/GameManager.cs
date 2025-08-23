using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private SkillConfig gameSkill;
    [SerializeField] private ItemConfig gameItem;
    [SerializeField] private PlayerData userData;

    public GameObject Character;
    public GameObject HighlightController;

    public Database<SkillCfgSkill> DB_Skill;
    public Database<ItemCfgItem> DB_Item;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        DB_Skill = new();
        DB_Item = new();
            
        DB_Skill.datas = gameSkill.GetRuntimeCopy();
        DB_Item.datas = gameItem.GetRuntimeCopy();

    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0; // Tắt VSync để FPS thực sự giới hạn bởi targetFrameRate

    }

}
