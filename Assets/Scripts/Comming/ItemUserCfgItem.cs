using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemUserCfgItem : ConfigItem
{
    public int idItem;
    public int Level;
    public ItemRarity Rarity;
    public int Quantity;

    [SerializeField] public List<Attribute> attributes = new();
    [JsonIgnore] public Dictionary<EAttribute, float> attrDict = new Dictionary<EAttribute, float>();


    internal void CopyFrom(ItemUserCfgItem other)
    {
        idItem = other.idItem;
        id = other.id;
        Rarity = other.Rarity;
        Level = other.Level;
        Quantity = ItemConfig.GetInstance.GetConfigItem(idItem).Stackable ? other.Quantity : 1;
        attributes = other.attributes;
        attrDict = other.attrDict;

        Init();
    }

    public void Init()
    {
        foreach (var attr in attributes)
        {
            attrDict[attr.attribute] = attr.value;
        }
    }
    public override void ApplyFromRow(IDictionary<string, object> row) { }

    public string GetDescription()
    {
        string des;
        ItemCfgItem item = ItemConfig.GetInstance.GetConfigItem(idItem);

        des = $"{item.Description}\n";

        foreach (var row in attrDict)
        {
            des += $"{row.Key.ToString()}: {row.Value}\n";
        }

        return des;
    }
}



