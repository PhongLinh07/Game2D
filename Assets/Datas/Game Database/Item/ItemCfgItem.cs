using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;



public enum ItemRarity
{
    Mortal = 0,       // Phàm phẩm
    Genuine = 1,      // Chân phẩm
    Superior = 2,     // Thượng phẩm
    Celestial = 3,    // Tiên phẩm
    Divine = 4        // Thần phẩm
}

[System.Serializable]
public class ItemCfgItem : ConfigItem
{
    public string Name;
    public bool Stackable;
    public Sprite Icon;
    public string Description;

    public override void ApplyFromRow(IDictionary<string, object> row)
    {
        id = row.ContainsKey("id") ? Convert.ToInt32(row["id"]) : -1;
        Name = row.ContainsKey("name") ? row["name"]?.ToString() : null;
        Stackable = row.ContainsKey("stackable") ? (bool)row["stackable"] : false;
        Icon = SpriteConfig.GetInstance.GetSprite(row["icon"].ToString());
        Description = row.ContainsKey("desc") ? row["desc"]?.ToString() : null;
       
    }

    public Dictionary<string, object> ToDict()
    {
        var row = new Dictionary<string, object>
        {
            ["id"] = id,
            ["name"] = Name,
            ["stackable"] = Stackable,
            ["icon"] = Icon != null ? Icon.name : null,
            ["desc"] = Description
        };

        return row;
    }

    internal void CopyFrom(ItemCfgItem other)
    {
        id = other.id;
        Name = other.Name;
        Stackable = other.Stackable;
        Icon = other.Icon;
        Description = other.Description;
    }
}


[System.Serializable]
public class EnhanceCfgItem : ConfigItem
{
    public int idItem;
    public int Level;
    public ItemRarity Rarity;
    public int Quantity;
    public int Atk;
    public int Def;


    internal void CopyFrom(EnhanceCfgItem other)
    {
        idItem = other.idItem;
        id = other.id;
        Rarity = other.Rarity;
        Level = other.Level;
        Quantity = ConfigMgr<ItemCfgItem>.GetInstance.GetConfigItem(idItem).Stackable ? other.Quantity : 1;
        Atk = other.Atk;
        Def = other.Def;
    }

    public override void ApplyFromRow(IDictionary<string, object> row) 
    {
        id          = row.ContainsKey("id") ? Convert.ToInt32(row["id"]) : -1;
        idItem      = row.ContainsKey("idItem") ? Convert.ToInt32(row["idItem"]) : -1;
        Level       = row.ContainsKey("Level") ? Convert.ToInt32(row["Level"]) : 0;
        Rarity      = row.ContainsKey("Rarity") ? (ItemRarity)(row["Rarity"]) : 0;
        Quantity    = row.ContainsKey("Quantity") ? Convert.ToInt32(row["Quantity"]) : 0;
        Atk         = row.ContainsKey("Atk") ? Convert.ToInt32(row["Atk"]) : 0;
        Def         = row.ContainsKey("Def") ? Convert.ToInt32(row["Def"]) : 0;

    }

    public Dictionary<string, object> ToDict()
    {
        var dict = new Dictionary<string, object>
        {
            ["id"] = id,
            ["idItem"] = idItem,
            ["Level"] = Level,
            ["Rarity"] = (int)Rarity,
            ["Quantity"] = Quantity,
            ["Atk"] = Atk,
            ["Def"] = Def,
        };
        return dict;
    }

    public string GetDescription()
     {
      string des;
      ItemCfgItem item = ConfigMgr < ItemCfgItem >.GetInstance.GetConfigItem(idItem);

      if (item.Stackable)
      {
          des = $"{item.Description}\n";
      }
      else
      {
          des =
              $"Level: {Level}\n" +
              $"{item.Description}\n" +
              $"Atk: {Atk}\n" +
              $"Def: {Def}\n";
      }  
      return des;
     }
}



