using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class EquipType
{
    public EEquipmentType equipType;
    public int id;
}


[System.Serializable]
public class CharacterCfgItem : ConfigItem // dữ liệu dưới dạng số
{
    public List<int> SkillsLearned = new();
    public List<int> SkillsEquipped = new();   // danh sách buff

    public List<int> items = new();   // danh sách
    public List<int> itemsEquipped = new();   // danh sách

    public List<Attribute> attributes = new();


    public float positionX = 0.0f;
    public float positionY = 0.0f;

    public EAsset idPrefab;

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