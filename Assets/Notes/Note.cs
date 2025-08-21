/*
    public RectTransform panelContent;
    public GameObject slotButton;
    public InventoryPanel inventoryPanel;

    public void test(skillSlot skillSlot)
    {
        GameObject newSlot = Instantiate(slotButton);
        newSlot.transform.SetParent(panelContent.transform, false);

        InventoryCell slot = newSlot.GetComponent<InventoryCell>();
        slot.icon.sprite = skillSlot.skill.Icon;
        slot.quantity.text = skillSlot.quantity.ToString();


        inventoryPanel.inventoryCells.Add(slot);
    }

    public void init(skillSlot skillSlot)
    {
        GameObject newSlot = Instantiate(slotButton);
        newSlot.transform.SetParent(panelContent.transform, false);

        InventoryCell slot = newSlot.GetComponent<InventoryCell>();
        slot.icon.sprite = skillSlot.skill.Icon;
        slot.quantity.text = skillSlot.quantity.ToString();


        inventoryPanel.inventoryCells.Add(slot);
    }
*/