using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[System.Serializable]
public class CharacterUnit : UnitStats // Dữ liệu dưới dạng object
{
    [SerializeField] private CharacterCfgItem data;

    public Dictionary<int, ItemUserCfgItem> items = new();
    public Dictionary<EEquipmentType, ItemUserCfgItem> quipDict = new Dictionary<EEquipmentType, ItemUserCfgItem>();
    public Dictionary<int, SkillCfgItem> SkillsLearnedDict = new();
    public Dictionary<int, SkillCfgItem> SkillsEquippedDict = new();   // danh sách buff
    public Dictionary<EAttribute, Attribute> attributes = new();   // danh sách buff


    private void Awake()
    {
        data = CharacterConfig.GetInstance.GetConfigItem(0);

        List<ItemUserCfgItem> itemsSO = ItemUserConfig.GetInstance.mDatas;
        foreach (var item in itemsSO)
        {
            items[item.id] = item;
        }

        foreach (var idItem in data.itemsEquipped)
        {
            quipDict[items[idItem].GetTemplate().equipType] = items[idItem];
        }

        foreach (var skill in data.SkillsLearned)
        {
            SkillsLearnedDict[skill] = SkillConfig.GetInstance.GetConfigItem(skill);
        }

        foreach (var skill in data.SkillsEquipped)
        {
            SkillsEquippedDict[skill] = SkillConfig.GetInstance.GetConfigItem(skill);
        }

        foreach (var attr in data.attributes)
        {
            attributes[attr.attribute] = attr;
        }
    }

    public CharacterCfgItem Data
    {
        get { return data; }       // bên ngoài chỉ đọc
        private set { data = value; } // chỉ class này mới gán
    }

    public Attribute GetAttributes(EAttribute attributeType)
    {
        return attributes[attributeType];
    }

    public void EquipSkill(int idSkill)
    {
        SkillsEquippedDict[idSkill] = SkillsLearnedDict[idSkill];
        if (!data.SkillsEquipped.Contains(idSkill))
        {
            data.SkillsEquipped.Add(idSkill);
        }
    }

    public void UnequipSkill(int idSkill)
    {
        SkillsEquippedDict.Remove(idSkill);
        if (data.SkillsEquipped.Contains(idSkill))
        {
            data.SkillsEquipped.Remove(idSkill);
        }
    }

    public void Equipment(EEquipmentType equipType, ItemUserCfgItem item)
    {
        if(quipDict.ContainsKey(equipType) && quipDict[equipType] != null) data.itemsEquipped.Remove(quipDict[equipType].id);
        quipDict[equipType] = item; 
        data.itemsEquipped.Add(item.id);
        
    }


    public void Unequipment(EEquipmentType equipType)
    {
        if (quipDict.ContainsKey(equipType) && quipDict[equipType] != null) data.itemsEquipped.Remove(quipDict[equipType].id);
        Debug.LogWarning(quipDict[equipType].id);
        quipDict.Remove(equipType);
    }

    public override void SetPosition(Vector2 position)
    {
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