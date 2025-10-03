using UnityEngine;
using UnityEngine.UIElements;



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


public class InventorySlotUI : ASlotUI
{
    public RarityConfigSO rarityCell;
    public ItemUserCfgItem dataOfSlot;

    public Label Quantity { get; private set; }

    private float doubleTapTime = 0.25f; // Khoảng thời gian tối đa giữa 2 lần tap
    private float lastTapTime = 0;
    public InventorySlotUI(VisualTreeAsset template, RarityConfigSO rarity) : base(template)
    {

        Quantity = Root.Q<Label>("Quantity");
        Quantity.style.display = DisplayStyle.None;
        rarityCell = rarity;

    }

    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            return;
        }

        dataOfSlot = data as ItemUserCfgItem;

        ItemCfgItem item = ItemConfig.GetInstance.GetConfigItem(dataOfSlot.id_Item);

        Background.style.backgroundImage = new StyleBackground(rarityCell.rarityDict[dataOfSlot.Rarity]);
        Icon.style.backgroundImage = new StyleBackground(item.Icon);

        if (item.Stackable)
        {
            Quantity.style.display = DisplayStyle.Flex;
            Quantity.text = dataOfSlot.Quantity.ToString();
        }
        else
        {
            Quantity.style.display = DisplayStyle.None;
        }

    }

   

    protected override void OnClick(ClickEvent evt)
    {
        Debug.Log("Slot Clicked");

        Inventory.Instance.OnClick(dataOfSlot.id);

        // ---------------- Double Tap logic ----------------
        if (Time.time < lastTapTime + doubleTapTime) TryEquipSkill();

        lastTapTime = Time.time;
    }


    private void TryEquipSkill()
    {
        Inventory.Instance.EquipItem(dataOfSlot);
    }
}
