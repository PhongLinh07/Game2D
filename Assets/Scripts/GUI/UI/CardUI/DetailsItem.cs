using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsItem : MonoBehaviour
{
    public RarityGUI data;
    public RarityGUI rarityCell;

    public Image headerCard;
    public Image bg;
    public Image icon;
    
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI descriptionItem;

    // Start is called before the first frame updatehuws trong scrip 
    public void SetData(EnhanceCfgItem playerItem)
    {
        headerCard.sprite = data.rarityDict[playerItem.Rarity];
        bg.sprite = rarityCell.rarityDict[playerItem.Rarity];
        icon.sprite = playerItem.Data.Icon;
        nameItem.text = playerItem.Data.Name;
        descriptionItem.text = playerItem.GetDescription();
    }
}
