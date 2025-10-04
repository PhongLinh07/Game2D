using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : AContainer<ItemUserCfgItem>
{
    public RarityConfigSO rarityCell;
    public RarityConfigSO data;

    public static Inventory Instance;
    private LogicCharacter _logicCharacter;

    private VisualElement Header;
    private VisualElement Preview;
    private VisualElement Icon;
    private Label Information;
    private Label Description;


    private void Awake()
    {
        Instance = this;
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }

    private void Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;
        _logicCharacter = logicCharacter;

        Init();
        UpdateContainer();
    }

    public override void Init()
    {
        base.Init();
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        // ScrollView có sẵn trong UXML
        var scroll = root.Q<VisualElement>("Btn_Inventory").Q<ScrollView>("ScrollView");
        slotUIs.Clear();


        // Tạo container grid bên trong
        var gridContainer = new VisualElement();
        gridContainer.style.flexDirection = FlexDirection.Row;
        gridContainer.style.flexWrap = Wrap.Wrap;
        // gridContainer.style.justifyContent = Justify.Center; // căn giữa grid
        gridContainer.style.flexGrow = 1;

        // Clear và add container vào ScrollView
        scroll.contentContainer.Clear();
        scroll.contentContainer.Add(gridContainer);



        foreach (var item in _logicCharacter.Data.itemsOwned)
        {
            var newSlot = new InventorySlotUI(slotTemplate, rarityCell);
            newSlot.SetData(item.Value);

            slotUIs[item.Key] = newSlot;
            gridContainer.Add(newSlot.Root);
        }




        var detail = root.Q<VisualElement>("Btn_Inventory").Q<VisualElement>("Detail");

        Header = detail.Q<VisualElement>("Head");
        Preview = detail.Q<VisualElement>("Preview");
        Icon = detail.Q<VisualElement>("Icon");
        Information = detail.Q<Label>("Information");
        Description = detail.Q<Label>("Description");

    }

    public override void UpdateContainer()
    {
        InventorySlotUI s;
        ItemUserCfgItem i;
        foreach (var slot in slotUIs)
        {
            s = (InventorySlotUI)slot.Value;
            i = s.dataOfSlot;

            if (_logicCharacter.Data.itemsEquipped.ContainsKey(i.GetTemplate().equipType))
            {
                s.Equip(_logicCharacter.Data.itemsEquipped[i.GetTemplate().equipType].id == i.id);
            }
            else
            {
                s.Equip(false);
            }


        }
    }
    public void EquipItem(ItemUserCfgItem item) //toggle
    {
        ItemCfgItem itemCfg = item.GetTemplate();

        if (itemCfg.equipType == EEquipmentType.None) return;

        _logicCharacter.Equipment(itemCfg.equipType, item);

        UpdateContainer();
    }



    public override void OnClick(int index)
    {
        base.OnClick(index);
        UpdateDetail(_logicCharacter.Data.itemsOwned[index]);
    }


    private void UpdateDetail(ItemUserCfgItem item)
    {
        Header.style.backgroundImage = new StyleBackground(data.rarityDict[item.Rarity]);
        Preview.style.backgroundImage = new StyleBackground(rarityCell.rarityDict[item.Rarity]);
        Icon.style.backgroundImage = new StyleBackground(ItemConfig.GetInstance.GetConfigItem(item.id_Item).Icon);
        Information.text =  ItemConfig.GetInstance.GetConfigItem(item.id_Item).Name;
        Description.text = item.GetDescription();
    }
}
