using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

[System.Serializable]
public class RarityCfItem : ConfigItem
{
    public ItemRarity itemRarity;
    public Sprite sprite;

    public override void ApplyFromRow(IDictionary<string, object> row) { }

    internal void CopyFrom(RarityCfItem other)
    {
        id = other.id;
        itemRarity = other.itemRarity;
        sprite = other.sprite;
    }
    public override string ToString()
    {
        return "";
    }

}

[CreateAssetMenu(menuName = "DataBase/Rarity")]
public class RarityConfigSO : ScriptableObject
{
    public List<RarityCfItem> datas;

    public Dictionary<ItemRarity, Sprite> rarityDict = new();

    private void OnEnable()
    {
        foreach (var rarity in datas)
        {
            rarityDict.Add(rarity.itemRarity, rarity.sprite);
        }
        
    }
}

