using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;

[System.Serializable]
public class CharacterUnit : UnitStats
{
    public LogicCharacter mlogic;

    public MartialArts martialArts;
    public SpiritualRoot spiritualRoot;

    public List<int> SkillsLearned = new(); // danh sách skill
    public List<int> SkillsEquipped = new();   // danh sách buff
    public List<EnhanceCfgItem> items = new();

    public void EquipSkill(int idSkill)
    {
        if (!SkillsEquipped.Contains(idSkill))
        {
            SkillsEquipped.Add(idSkill);
        }
    }

    public void UnequipSkill(int idSkill)
    {
        if (SkillsEquipped.Contains(idSkill))
        {
            SkillsEquipped.Remove(idSkill);
        }
    }

}