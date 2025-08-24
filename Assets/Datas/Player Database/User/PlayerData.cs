using System.Collections.Generic;

/// <summary>
/// Runtime
/// </summary>

[System.Serializable]
public class PlayerData : ConfigItem<PlayerData>
{
    //Method
    public string name;
    public List<int> skillsLearned;
    public List<int> skillsEquipped;
    public List<EnhanceCfgItem> items;

    public PlayerData()
    {
        name = "Vuong That Phong";
        skillsLearned = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7}; //id
        skillsEquipped = new List<int>();


        EnhanceCfgItem test = new EnhanceCfgItem();
        test.Id = 0;
        items = new List<EnhanceCfgItem> { test }; // Id(Only), quantity
    }
    //Attribute
    public override void CopyFrom(PlayerData other)
    {
        Id = other.Id;
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
