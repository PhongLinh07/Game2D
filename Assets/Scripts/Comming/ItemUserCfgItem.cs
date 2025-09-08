using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemUserCfgItem : ConfigItem
{
    public int id_Item;
    public int Level = 0;
    public ItemRarity Rarity;
    public int Quantity = 1;

    [SerializeField] public List<Attribute> attributes = new();
    [JsonIgnore] public Dictionary<EAttribute, float> attrDict = new Dictionary<EAttribute, float>();

    public ItemUserCfgItem() { }

    public ItemUserCfgItem(ItemUserCfgItem other)
    {
        id = other.id; // id of ItemTemplate
        id_Item = other.id_Item;
        Rarity = other.Rarity;
        Level = other.Level;
        Quantity = ItemConfig.GetInstance.GetConfigItem(id).Stackable ? other.Quantity : 1;
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
        ItemCfgItem item = ItemConfig.GetInstance.GetConfigItem(id_Item);

        des = $"{item.Description}\n";

        foreach (var row in attrDict)
        {
            des += $"{row.Key.ToString()}: {row.Value}\n";
        }

        return des;
    }

    public ItemCfgItem GetTemplate() // return item template of this palyerItem
    {
        return ItemConfig.GetInstance.GetConfigItem(id_Item);
    }
}



