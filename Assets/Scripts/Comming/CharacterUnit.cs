using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class CharacterUnit : UnitStats
{
    [SerializeField] private CharacterCfgItem data;

    private void Awake()
    {
        data = CharacterConfig.GetInstance.GetConfigItem(0);
    }

    public CharacterCfgItem Data
    {
        get { return data; }       // bên ngoài chỉ đọc
        private set { data = value; } // chỉ class này mới gán
    }

    public void EquipSkill(int idSkill)
    {
        if (!data.SkillsEquipped.Contains(idSkill))
        {
            data.SkillsEquipped.Add(idSkill);
        }
    }

    public void UnequipSkill(int idSkill)
    {
        if (data.SkillsEquipped.Contains(idSkill))
        {
            data.SkillsEquipped.Remove(idSkill);
        }
    }

    public void Equipment(EEquipType equipType, int idItem)
    {
        data.quipDict[equipType] = idItem;
    }


    public void Unequipment(EEquipType equipType)
    {
        data.quipDict.Remove(equipType);
    }

    public override void SetPosition(Vector2 position)
    {
        data.position = position;
    }
    public override int TakeDamage(int damage)
    {
        return data.general.currVitality = Mathf.Clamp(data.general.currVitality - damage, 0, data.general.vitality);
    }

    public override int Heal(int amount)
    {
        return data.general.currVitality = Mathf.Clamp(data.general.currVitality + amount, 0, data.general.vitality);
    }

}