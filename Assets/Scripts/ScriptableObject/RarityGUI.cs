using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class RarityCell : ConfigItem<RarityCell>
{
    public ItemRarity itemRarity;
    public Sprite sprite;

    public override void CopyFrom(RarityCell other) { }

}

[CreateAssetMenu(menuName = "Data/UI/RarityGUI")]
public class RarityGUI : ConfigBase<RarityCell>
{
    public Dictionary<ItemRarity, Sprite> rarityDict = new();

    private void OnEnable()
    {
        foreach (var rarity in datas)
        {
            rarityDict.Add(rarity.itemRarity, rarity.sprite);
        }
        
    }
}

