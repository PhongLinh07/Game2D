using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;

[System.Serializable]
public class CharacterUnit : UnitStats
{
    public LogicCharacter mlogic;

    public ChacterCfgItem data;

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

}