using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsItem : MonoBehaviour
{
    public RarityConfigSO data;
    public RarityConfigSO rarityCell;

    public Image headerCard;
    public Image bg;
    public Image icon;
    
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI descriptionItem;

    // Start is called before the first frame updatehuws trong scrip 
    public void SetData(ItemUseCfgItem playerItem)
    {
        headerCard.sprite = data.rarityDict[playerItem.Rarity];
        bg.sprite = rarityCell.rarityDict[playerItem.Rarity];
        icon.sprite = ItemConfig.GetInstance.GetConfigItem(playerItem.idItem).Icon;
        nameItem.text = ItemConfig.GetInstance.GetConfigItem(playerItem.idItem).Name;
        descriptionItem.text = playerItem.GetDescription();
    }
}
