using System.Collections;
using System.Collections.Generic;
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
public class ItemCfgItem : ConfigItem<ItemCfgItem>
{
    public string Name;
    public bool Stackable;
    public Sprite Icon;
    public string Description;

    public override void CopyFrom(ItemCfgItem item)
    {
        Id = item.Id;
        Name = item.Name;
        Stackable = item.Stackable;
        Icon = item.Icon;
        Description = item.Description;
    }
}

[CreateAssetMenu(menuName = "DataBase/Item")]
public class ItemConfig : ConfigBase<ItemCfgItem> { }


[System.Serializable]
public class EnhanceCfgItem : ConfigItem<EnhanceCfgItem>
{ 
    public ItemCfgItem Data;     
    public int Level;
    public ItemRarity Rarity;
    public int Quantity;
    public int Atk;
    public int Def;


    public override void CopyFrom(EnhanceCfgItem other)
    {
        Data = other.Data;
        Id = other.Id;
        Rarity = other.Rarity;
        Level = other.Level;
        Quantity = Data.Stackable ? other.Quantity : 1;
        Atk = other.Atk;
        Def = other.Def;
    }

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


