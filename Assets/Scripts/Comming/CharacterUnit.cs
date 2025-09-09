using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class CharacterUnit : UnitStats // Dữ liệu dưới dạng object
{
    [SerializeField] private CharacterCfgItem data;

    public Dictionary<int, ItemUserCfgItem> itemsOwned = new();
    public Dictionary<EEquipmentType, ItemUserCfgItem> itemsEquipped = new Dictionary<EEquipmentType, ItemUserCfgItem>();
    public Dictionary<int, SkillCfgItem> SkillsLearned = new();
    public Dictionary<int, SkillCfgItem> SkillsEquipped = new();   // danh sách buff
    public Dictionary<EAttribute, Attribute> attributes = new();   // danh sách buff



    private void Awake()
    {
        data = CharacterConfig.GetInstance.GetConfigItem(0);

        List<ItemUserCfgItem> itemsSO = ItemUserConfig.GetInstance.mDatas;
        foreach (var item in itemsSO)
        {
            itemsOwned[item.id] = item;
        }

        foreach (var idItem in data.itemsEquipped)
        {
            itemsEquipped[itemsOwned[idItem].GetTemplate().equipType] = itemsOwned[idItem];
        }

        foreach (var skill in data.SkillsLearned)
        {
            SkillsLearned[skill] = SkillConfig.GetInstance.GetConfigItem(skill);
        }

        foreach (var skill in data.SkillsEquipped)
        {
            SkillsEquipped[skill] = SkillConfig.GetInstance.GetConfigItem(skill);
        }

        foreach (var attr in data.attributes)
        {
            attributes[attr.attribute] = attr;
        }
    }


    public Attribute GetAttributes(EAttribute attributeType)
    {
        return attributes[attributeType];
    }


    public bool EquipSkill(SkillCfgItem skill)
    {
        if (SkillsEquipped.Count >= 7) return false;

        // if existed => Unequip
        if (SkillsEquipped.ContainsKey(skill.id))
        {
            UnequipSkill(skill);
            return false;
        }

        // not yet equip then check EWeaponType
        itemsEquipped.TryGetValue(EEquipmentType.Weapon, out var item);

        // if skill need weapon
        if (skill.weaponType != EWeaponType.None) 
        {
            //if not yet equip weapon or Weapons do not meet requirements
            if (GetWeaponCurrent() == EWeaponType.None || skill.weaponType != item.GetTemplate().weaponType)
            {  
                Debug.LogWarning($"This skill need weapon: {skill.weaponType.ToString()}");
                return false;
            }
        }

        SkillsEquipped[skill.id] = SkillsLearned[skill.id];

        //sync
        if (!data.SkillsEquipped.Contains(skill.id))
        {
            data.SkillsEquipped.Add(skill.id);
        }
        return true;
    }


    public void UnequipSkill(SkillCfgItem skill)
    {
        SkillsEquipped.Remove(skill.id);

        //sync
        if (data.SkillsEquipped.Contains(skill.id))
        {
            data.SkillsEquipped.Remove(skill.id);
        }
    }


    public EWeaponType GetWeaponCurrent()
    {
        //if equipped
        if (itemsEquipped.TryGetValue(EEquipmentType.Weapon, out var value) && value != null)
        {
            return value.GetTemplate().weaponType;
        }

        return EWeaponType.None;
    }

    public bool Equipment(EEquipmentType equipType, ItemUserCfgItem item)
    {
        ItemCfgItem template = item.GetTemplate();

        if (template.equipType == EEquipmentType.None) return false;

        itemsEquipped.TryGetValue(equipType, out var value);

        if (value != null && value.id == item.id) // nếu là lần 2 thì đảo ngược
        {
            Unequipment(equipType);
            return false;
        }

        //sync
        if (value != null)
        {
            data.itemsEquipped.Remove(value.id);
        }

        itemsEquipped[equipType] = item; 
        data.itemsEquipped.Add(item.id);

        IsWeaponValid(GetWeaponCurrent());

        return true;
    }

    public void Unequipment(EEquipmentType equipType)
    {
        //sync
        if (itemsEquipped.TryGetValue(equipType, out var value) && value != null)
        {
            data.itemsEquipped.Remove(value.id);
        }
  
        itemsEquipped.Remove(equipType);

        IsWeaponValid(GetWeaponCurrent());
    }

    public void IsWeaponValid(EWeaponType weaponType)
    {
        foreach (var pair in SkillsEquipped.ToList())
        {
            if(pair.Value.weaponType != EWeaponType.None && pair.Value.weaponType != weaponType)
            {
                UnequipSkill(pair.Value);
            }
        }
    }

    public override void SetPosition(Vector2 position)
    {
        //sysnc
        data.position = position;
    }
    public override int TakeDamage(int damage)
    {
        return (int)(attributes[EAttribute.Hp].currValue = Mathf.Clamp(attributes[EAttribute.Hp].currValue - damage, 0, attributes[EAttribute.Hp].value));
    }

    public override int Heal(int amount)
    {
        return (int)(attributes[EAttribute.Hp].currValue = Mathf.Clamp(attributes[EAttribute.Hp].currValue + amount, 0, attributes[EAttribute.Hp].value));
    }

    public override int UseSkill(int idSkill)
    {
        float qiConsumption = SkillConfig.GetInstance.GetConfigItem(idSkill).attrDict[EAttribute.EnergyCost];
        return (int)(attributes[EAttribute.Mana].currValue = Mathf.Clamp(attributes[EAttribute.Hp].currValue - qiConsumption, 0, attributes[EAttribute.Mana].value));
    }

}