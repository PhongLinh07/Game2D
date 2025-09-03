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
    public EEquipType equipType;
    public EWeaponType weaponType;

    public override void ApplyFromRow(IDictionary<string, object> row) { }

    internal void CopyFrom(ItemCfgItem other)
    {
        id = other.id;
        Name = other.Name;
        Stackable = other.Stackable;
        Icon = other.Icon;
        Description = other.Description;
        equipType = other.equipType;
        weaponType = other.weaponType;
    }
}


[System.Serializable]
public class ItemUseCfgItem : ConfigItem
{
    public int idItem;
    public int Level;
    public ItemRarity Rarity;
    public int Quantity;
    public int Atk;
    public int Def;


    internal void CopyFrom(ItemUseCfgItem other)
    {
        idItem = other.idItem;
        id = other.id;
        Rarity = other.Rarity;
        Level = other.Level;
        Quantity = ItemConfig.GetInstance.GetConfigItem(idItem).Stackable ? other.Quantity : 1;
        Atk = other.Atk;
        Def = other.Def;
    }

    public override void ApplyFromRow(IDictionary<string, object> row) { }
   
    public string GetDescription()
     {
      string des;
      ItemCfgItem item = ItemConfig.GetInstance.GetConfigItem(idItem);

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



