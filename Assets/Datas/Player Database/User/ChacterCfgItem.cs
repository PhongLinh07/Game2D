using Newtonsoft.Json;
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



    public override void ApplyFromRow(IDictionary<string, object> row) { }
   
}