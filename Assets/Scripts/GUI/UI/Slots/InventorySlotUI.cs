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

public class InventorySlotUI : ASlotUI, IPointerClickHandler
{
    public RarityConfigSO rarityCell;
    public ItemUserCfgItem dataOfSlot;
    private float doubleTapTime = 0.25f; // Khoảng thời gian tối đa giữa 2 lần tap
    private float lastTapTime = 0;

    private Inventory inventory;      // ScriptableObject chứa info skill

    private void Awake()
    {
        inventory = Inventory.Instance;      // ScriptableObject chứa info skill
        base.Init();
    }

    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            Reset();
            return;
        }

        dataOfSlot = data as ItemUserCfgItem;

        ItemCfgItem item = ItemConfig.GetInstance.GetConfigItem(dataOfSlot.idItem);
        gameObject.SetActive(true);
        background.sprite = rarityCell.rarityDict[dataOfSlot.Rarity];
        icon.sprite = item.Icon;
        
        if (item.Stackable)
        {
            countText.gameObject.SetActive(true);
            countText.text = dataOfSlot.Quantity.ToString();
        }
        else
        {
            countText.gameObject.SetActive(false);
        }

    }

    public void Equip(bool state)
    {
        tick.gameObject.SetActive(state);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        AContainer<ItemUserCfgItem> container = transform.parent.GetComponent<AContainer<ItemUserCfgItem>>();
        // Gọi hàm Instance đã override từ hàm ảo ItemPanel
        container.OnClick(dataOfSlot.id);

        // ---------------- Double Tap logic ----------------
        if (Time.time < lastTapTime + doubleTapTime) TryEquipSkill();

        lastTapTime = Time.time;
    }

    private void TryEquipSkill()
    {
        if (inventory.EquipItem(dataOfSlot))
            tick.gameObject.SetActive(true);
        else
            tick.gameObject.SetActive(false);
    }
}
