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
    private LogicCharacter _logicCharacter;

    public static BattleSkillManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;

    }



    private void Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;
        _logicCharacter = logicCharacter;

        for (int i = 0; i < _logicCharacter.Data.SkillsEquipped.Count; i++)
        {
            SkillCfgItem skill = SkillConfig.GetInstance.GetConfigItem(_logicCharacter.Data.SkillsEquipped[i]);
            if (skill == null)
            {
                Debug.Log("skill Null");
                continue;
            }

            ISkillButton newSkillButton = GetSkillButton((SkillInputType)skill.InputType);

            newSkillButton.gameObject.SetActive(true);
            newSkillButton.SetData(_logicCharacter, skill);
            skillButtons.Add(skill.id, newSkillButton);
        }
    }

    public bool EquipSKill(SkillCfgItem skill)
    {
        if (_logicCharacter.Data.SkillsEquipped.Contains(skill.id)) // nếu đã tồn tại thì remove
        {
            _logicCharacter.UnequipSkill(skill.id); 
            if (skillButtons.ContainsKey(skill.id))
            {
                Destroy(skillButtons[skill.id].gameObject);
                skillButtons.Remove(skill.id);
            }
            return false;
        }
        else
        {
            if (_logicCharacter.Data.SkillsEquipped.Count >= 7) return false;

            _logicCharacter.EquipSkill(skill.id); // gán skill vào list
            skillButtons[skill.id] = GetSkillButton((SkillInputType)skill.InputType);
            skillButtons[skill.id].gameObject.SetActive(true);
            skillButtons[skill.id].SetData(_logicCharacter, skill);
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
