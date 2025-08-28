using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ChacterCfgItem : ConfigItem
{
    public Information infomation = new();
    public General general = new();
    public Combat combat = new();

    public MartialArts martialArts = new();
    public SpiritualRoot spiritualRoot = new();

    public List<int> SkillsLearned = new();
    public List<int> SkillsEquipped = new();   // danh sách buff
    public List<EnhanceCfgItem> items = new();

    public override void ApplyFromRow(IDictionary<string, object> row)
    {
        JObject jObject = JObject.FromObject(row); // đảm bảo nested object đúng kiểu
        var data = jObject.ToObject<ChacterCfgItem>();

        if (data == null) return;

        this.id = data.id;
        this.infomation = data.infomation;
        this.general = data.general;
        this.combat = data.combat;
        this.martialArts = data.martialArts;
        this.spiritualRoot = data.spiritualRoot;
        this.SkillsLearned = data.SkillsLearned ?? new List<int>();
        this.SkillsEquipped = data.SkillsEquipped ?? new List<int>();
        this.items = data.items ?? new List<EnhanceCfgItem>();
    }



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
