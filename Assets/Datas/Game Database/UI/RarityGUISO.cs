using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class RarityGUI
{
    public ItemRarity itemRarity;
    public Sprite sprite;

}

[CreateAssetMenu(menuName = "Data/UI/RarityGUI")]
public class RarityGUISO : ScriptableObject
{
    public List<RarityGUI> raritys;
    public Dictionary<ItemRarity, Sprite> rarityDict = new();

    private void OnEnable()
    {
        foreach (var rarity in raritys)
        {
            rarityDict.Add(rarity.itemRarity, rarity.sprite);
        }
        
    }
}

