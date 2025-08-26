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
    public ItemCfgItem Data;
    public int Level;
    public ItemRarity Rarity;
    public int Quantity;
    public int Atk;
    public int Def;


    internal void CopyFrom(EnhanceCfgItem other)
    {
        Data = other.Data;
        id = other.id;
        Rarity = other.Rarity;
        Level = other.Level;
        Quantity = Data.Stackable ? other.Quantity : 1;
        Atk = other.Atk;
        Def = other.Def;
    }

    public override void ApplyFromRow(IDictionary<string, object> row) { }

    public string GetDescription()
     {
      string des;
      
      if(Data.Stackable)
      {
          des = $"{Data.Description}\n";
      }
      else
      {
          des =
              $"Level: {Level}\n" +
              $"{Data.Description}\n" +
              $"Atk: {Atk}\n" +
              $"Def: {Def}\n";
      }  
      return des;
     }
}



