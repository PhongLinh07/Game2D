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
    public RarityGUI rarityCell;
    public Image bg;
    public TextMeshProUGUI quantity;

    public PlayerItem dataOfSlot;

    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            Reset();
            return;
        }
        dataOfSlot = data as PlayerItem;
        view.SetActive(true);
        bg.sprite = rarityCell.rarityDict[dataOfSlot.Rarity];
        icon.sprite = dataOfSlot.Data.Icon;
        
        if (dataOfSlot.Data.Stackable)
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
        AContainer<PlayerItem> container = transform.parent.GetComponent<AContainer<PlayerItem>>();
        // Gọi hàm Instance đã override từ hàm ảo ItemPanel
        container.OnClick(thisIndex);
    }


}
