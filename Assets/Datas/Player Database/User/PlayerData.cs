using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Runtime
/// </summary>

[System.Serializable]
public class PlayerData : ConfigItem
{
    //Method
    public string name;
    public List<int> skillsLearned;
    public List<int> skillsEquipped;
    public List<EnhanceCfgItem> items;

    public PlayerData()
    {
        name = "Vuong That Phong";
        skillsLearned = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }; //id
        skillsEquipped = new List<int>();



        items = new List<EnhanceCfgItem>
        {
            new EnhanceCfgItem
            {
                id = 0,
                Data = new ItemCfgItem { id = 0 },
                Rarity = (ItemRarity)0,
            },
            new EnhanceCfgItem
            {
                id = 2,
                Data = new ItemCfgItem { id = 3 },
                Rarity = (ItemRarity)2
            },
            new EnhanceCfgItem
            {
                id = 3,
                Data = new ItemCfgItem { id = 5 },
                Rarity = (ItemRarity)4
            }
        };

    }

    public override void ApplyFromRow(IDictionary<string, object> row) { }

    //Attribute
    public void CopyFrom(PlayerData other)
    {
        id = other.id;
        name = other.name;
        skillsLearned = other.skillsLearned;
        skillsEquipped = other.skillsEquipped;
        items = other.items;
    }

    public void SkillEuipped(int idSkill)
    {
        skillsEquipped.Add(idSkill);
        SaveSystem.Save<PlayerData>("player", this);
    }

    public void SkillRemoveEuipped(int idSkill)
    {
        skillsEquipped.Remove(idSkill);
        SaveSystem.Save<PlayerData>("player", this);
    }
}
