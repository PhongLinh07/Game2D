using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class CharacterCfgItem : ConfigItem
{
    public Information infomation = new();
    public General general = new();
    public Combat combat = new();
    public MartialArts martialArts = new();
    public SpiritualRoot spiritualRoot = new();

    public List<int> SkillsLearned = new();
    public List<int> SkillsEquipped = new();   // danh sách buff
    public List<EnhanceCfgItem> items = new();

    public float positionX = 0.0f;
    public float positionY = 0.0f;

    [JsonIgnore] // tránh serialize thẳng Vector2
    public Vector2 position
    {
        get
        {
            return new Vector2(positionX, positionY);
        }
        set
        {
            positionX = value.x;
            positionY = value.y;
        }
    }

    public override void ApplyFromRow(IDictionary<string, object> row) { }
   
}