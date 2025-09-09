using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;

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
        _logicCharacter.OnWeaponChanged += IsWeaponValid;

        foreach (var skill in _logicCharacter.Data.SkillsEquipped)
        {
            if (skill.Value == null)
            {
                Debug.Log("skill Null");
                continue;
            }

            ISkillButton newSkillButton = GetSkillButton((SkillInputType)skill.Value.InputType);

            newSkillButton.gameObject.SetActive(true);
            newSkillButton.SetData(_logicCharacter, skill.Value);
            skillButtons[skill.Value.id] = newSkillButton;
        }
    }

    public bool EquipSKill(SkillCfgItem skill)
    {
        // if equiped succes
        if (_logicCharacter.EquipSkill(skill))
        {
            skillButtons[skill.id] = GetSkillButton((SkillInputType)skill.InputType);
            skillButtons[skill.id].gameObject.SetActive(true);
            skillButtons[skill.id].SetData(_logicCharacter, skill);

            return true;
        }

        // if equiped don't succes
        if (skillButtons.TryGetValue(skill.id, out var slot) && slot)
        {
            Destroy(slot.gameObject);
            skillButtons.Remove(skill.id);
        }
        return false;
    }

    // Call when OnWeaponChanged:
    private void IsWeaponValid(EWeaponType weaponType)
    {
        foreach (var pair in skillButtons.ToList())
        {
            if (pair.Value.data.weaponType != EWeaponType.None && pair.Value.data.weaponType != weaponType)
            {
                Destroy(pair.Value.gameObject);
                skillButtons.Remove(pair.Key);
            }
        }
    }


    private ISkillButton GetSkillButton(SkillInputType type)
    {
        ISkillButton skillButton;

        switch (type)
        {
            case SkillInputType.Tap: skillButton = Instantiate(skillButtonPrefab, parent).GetComponent<ISkillButton>(); break;
            case SkillInputType.Drag: skillButton = Instantiate(dragSkillButtonPrefab, parent).GetComponent<ISkillButton>(); break;
            case SkillInputType.Rotate: skillButton = Instantiate(rotateSkillButtonPrefab, parent).GetComponent<ISkillButton>(); break;
            default: skillButton = null; break;
        }

        return skillButton;
    }
}
