using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BattleSkillManager : MonoBehaviour
{
    [Header("GameObject chứa danh sách buttons")]
    public Transform parent;

    [Header("Danh sách các nút skill")]
    //public List<ISkillButton> skillButtons = new();
    public Dictionary<int, ISkillButton> skillButtons = new();

    [Header("Data of Player")]
    public PlayerData player;

    
    [Header("Skill button UI Prefab")]

    public GameObject skillButtonPrefab;
    public GameObject dragSkillButtonPrefab;
    public GameObject rotateSkillButtonPrefab;

    [SerializeField]
    private Transform transOfPlayer;

    public static BattleSkillManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for (int i = 0; i < player.skillEquipped.Count; i++)
        {
            SkillCfgSkill skill = GameManager.Instance.DB_Skill.GetById(player.skillEquipped[i]);
            if (skill == null)
            {
                Debug.Log("skill Null");
                continue;
            }

            ISkillButton newSkillButton = GetSkillButton(skill.InputType);

            newSkillButton.gameObject.SetActive(true);
            newSkillButton.SetData(transOfPlayer, skill);
            skillButtons.Add(skill.Id, newSkillButton);
        }
    }

    public bool EquipSKill(SkillCfgSkill skill)
    {
        if (player.skillEquipped.Contains(skill.Id)) // nếu đã tồn tại thì remove
        {
            player.skillEquipped.Remove(skill.Id); 
            if (skillButtons.ContainsKey(skill.Id))
            {
                Destroy(skillButtons[skill.Id].gameObject);
                skillButtons.Remove(skill.Id);
            }
            return false;
        }
        else
        {
            if (player.skillEquipped.Count >= 7) return false;

            player.skillEquipped.Add(skill.Id); // gán skill vào list
            skillButtons[skill.Id] = GetSkillButton(skill.InputType);
            skillButtons[skill.Id].gameObject.SetActive(true);
            skillButtons[skill.Id].SetData(transOfPlayer, skill);
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
