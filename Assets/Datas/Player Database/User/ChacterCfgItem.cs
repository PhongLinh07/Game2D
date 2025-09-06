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
    public string uuid;
}


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

    public List<ItemUserCfgItem> items = new();


    [SerializeField] public List<EquipType> equipTypes = new();
    [JsonIgnore] public Dictionary<EEquipmentType, ItemUserCfgItem> quipDict = new Dictionary<EEquipmentType, ItemUserCfgItem>();
    //[JsonProperty] public Dictionary<EEquipmentType, int> quipDict = new Dictionary<EEquipmentType, int>();


    public List<Attribute> attributes = new();
    [JsonIgnore] public Dictionary<EAttribute, float> attrDict = new Dictionary<EAttribute, float>();


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

    public void Init()
    {

        items = ItemUserConfig.GetInstance.mDatas;

        foreach (var att in attributes)
        {
            attrDict[att.attribute] = att.value;
            Debug.LogWarning($"{att.attribute.ToString()} --- {att.value}");
        }

        foreach (var item in equipTypes)
        {
            quipDict[item.equipType] = items.Find(i => i.uuid == item.uuid);
            Debug.LogWarning($"{item.equipType.ToString()} --- {item}");
        }

    }

    public override void ApplyFromRow(IDictionary<string, object> row) { }
    
}