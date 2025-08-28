using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Slot or Cell
/// This is elementUI of Inventory
/*
 *  Slot( Button, Script_SlotUI)
 *    |
 *    |___View
 *    |     |___BG( Image )
 *    |     |___Icon( Image )
 *    |     |___Quantity( Text )
 *    |
 *    |___Highlight( Image )
*/
/// </summary>

public class ItemSlotUI : ASlotUI, IPointerClickHandler
{
    public RarityConfigSO rarityCell;
    public Image bg;
    public TextMeshProUGUI quantity;

    public EnhanceCfgItem dataOfSlot;

    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            Reset();
            return;
        }
        dataOfSlot = data as EnhanceCfgItem;
        view.SetActive(true);
        bg.sprite = rarityCell.rarityDict[dataOfSlot.Rarity];
        icon.sprite = ConfigMgr<ItemCfgItem>.GetInstance.GetConfigItem(dataOfSlot.idItem).Icon;
        
        if (ConfigMgr<ItemCfgItem>.GetInstance.GetConfigItem(dataOfSlot.idItem).Stackable)
        {
            quantity.gameObject.SetActive(true);
            quantity.text = dataOfSlot.Quantity.ToString();
        }
        else
        {
            quantity.gameObject.SetActive(false);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AContainer<EnhanceCfgItem> container = transform.parent.GetComponent<AContainer<EnhanceCfgItem>>();
        // Gọi hàm Instance đã override từ hàm ảo ItemPanel
        container.OnClick(dataOfSlot.id);
    }


}
