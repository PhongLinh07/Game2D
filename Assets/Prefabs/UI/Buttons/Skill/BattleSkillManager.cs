using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleSkillManager : MonoBehaviour
{
    [SerializeField] protected UIDocument uiDocument;
    [SerializeField] protected VisualTreeAsset uiTemplate;
    [SerializeField] protected VisualElement root;

    public Dictionary<int, ISkillButton> skillButtons = new();

    
    [SerializeField]
    private LogicCharacter _logicCharacter;

    public static BattleSkillManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;

        uiDocument = GetComponent<UIDocument>();

        root = uiDocument.rootVisualElement.Q<VisualElement>("SkillButtons");
       
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
            newSkillButton.SetData(_logicCharacter, skill.Value);
            skillButtons[skill.Value.id] = newSkillButton;
            root.Add(newSkillButton.Root);
        }
    }

    public bool EquipSKill(SkillCfgItem skill)
    {
        // if equiped succes
        if (_logicCharacter.EquipSkill(skill))
        {
            skillButtons[skill.id] = GetSkillButton((SkillInputType)skill.InputType);
            skillButtons[skill.id].Root.style.display = DisplayStyle.Flex;
            skillButtons[skill.id].SetData(_logicCharacter, skill);
            root.Add(skillButtons[skill.id].Root);

            return true;
        }

        // if equiped don't succes
        if (skillButtons.TryGetValue(skill.id, out var slot) && slot)
        {
            root.Remove(slot.Root);
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
                root.Remove(pair.Value.Root);
                skillButtons.Remove(pair.Key);
            }
        }
    }


    private ISkillButton GetSkillButton(SkillInputType type)
    {
        ISkillButton skillButton;

        switch (type)
        {
            case SkillInputType.Drag: skillButton = new DragSkillButton(uiTemplate); break;
            case SkillInputType.Rotate: skillButton = new RotateSkillButton(uiTemplate); break;
            default: skillButton = null; break;
        }

        return skillButton;
    }
}
