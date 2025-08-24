using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleSkillManager : MonoBehaviour
{
    [Header("GameObject chứa danh sách buttons")]
    public Transform parent;

    [Header("Danh sách các nút skill")]
    //public List<ISkillButton> skillButtons = new();
    public Dictionary<int, ISkillButton> skillButtons = new();

    [Header("Data of Player")]
   // public PlayerData player;

    
    [Header("Skill button UI Prefab")]

    public GameObject skillButtonPrefab;
    public GameObject dragSkillButtonPrefab;
    public GameObject rotateSkillButtonPrefab;

    [SerializeField]
    private ObjectState oStatePlayer;

    public static BattleSkillManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.R_PlayerData.skillsEquipped.Count; i++)
        {
            SkillCfgSkill skill = GameManager.Instance.DB_Skill.GetById(GameManager.Instance.R_PlayerData.skillsEquipped[i]);
            if (skill == null)
            {
                Debug.Log("skill Null");
                continue;
            }

            ISkillButton newSkillButton = GetSkillButton(skill.InputType);

            newSkillButton.gameObject.SetActive(true);
            newSkillButton.SetData(oStatePlayer, skill);
            skillButtons.Add(skill.Id, newSkillButton);
        }
    }

    public bool EquipSKill(SkillCfgSkill skill)
    {
        if (GameManager.Instance.R_PlayerData.skillsEquipped.Contains(skill.Id)) // nếu đã tồn tại thì remove
        {
            GameManager.Instance.R_PlayerData.SkillRemoveEuipped(skill.Id); 
            if (skillButtons.ContainsKey(skill.Id))
            {
                Destroy(skillButtons[skill.Id].gameObject);
                skillButtons.Remove(skill.Id);
            }
            return false;
        }
        else
        {
            if (GameManager.Instance.R_PlayerData.skillsEquipped.Count >= 7) return false;

            GameManager.Instance.R_PlayerData.SkillEuipped(skill.Id); // gán skill vào list
            skillButtons[skill.Id] = GetSkillButton(skill.InputType);
            skillButtons[skill.Id].gameObject.SetActive(true);
            skillButtons[skill.Id].SetData(oStatePlayer, skill);
            return true;
        }

        
    }

    private ISkillButton GetSkillButton(SkillInputType type)
    {
        ISkillButton skillButton;

        switch(type)
        {
            case SkillInputType.Tap: skillButton = Instantiate(skillButtonPrefab, parent).GetComponent<ISkillButton>(); break;
            case SkillInputType.Drag: skillButton = Instantiate(dragSkillButtonPrefab, parent).GetComponent<ISkillButton>(); break;
            case SkillInputType.Rotate: skillButton = Instantiate(rotateSkillButtonPrefab, parent).GetComponent<ISkillButton>(); break;
            default: skillButton = null; break;
        }

        return skillButton;
    }
}
