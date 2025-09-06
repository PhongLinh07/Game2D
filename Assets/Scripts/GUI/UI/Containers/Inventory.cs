using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : AContainer<ItemUserCfgItem>
{
    public DetailsItem details;

    public static Inventory Instance;

    private LogicCharacter _logicCharacter;

    private List<InventorySlotUI> slots = new();

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }

    private void  Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;
        _logicCharacter = logicCharacter;

    }

    public override void Init()
    {
        _logicCharacter = LogicCharacter.Instance;
        slotUIs.Clear();

        for(int i = 0; i < _logicCharacter.Data.items.Count; i++)
        {
            InventorySlotUI newSlot = Instantiate(slotPrefab, parent).GetComponent<InventorySlotUI>();
            newSlot.SetIndex(i);
            newSlot.SetData<ItemUserCfgItem>(_logicCharacter.Data.items[i]);
            slots.Add(newSlot);

        }

        
        isInitialized = true;

        base.Init();
    }

    public void EquipItem(ItemUserCfgItem item)
    {
        ItemCfgItem itemCfg = ItemConfig.GetInstance.GetConfigItem(item.id);

        if(itemCfg.equipType == EEquipmentType.None) return;

        if (!_logicCharacter.Data.quipDict.ContainsKey(itemCfg.equipType)) // nếu key chưa tồn tại
        {
            _logicCharacter.Equipment(itemCfg.equipType, item);
          

        }
        else // nếu key đã tồn tại
        {
            if(_logicCharacter.Data.quipDict[itemCfg.equipType] != null) // nếu là lần 2 thì đảo ngược
            {
                _logicCharacter.Unequipment(itemCfg.equipType); 
            }

            _logicCharacter.Equipment(itemCfg.equipType, item);
        }

        UpdateContainer();
    }
    public override void UpdateContainer()
    {
        foreach(var slot in slots)
        {
            slot.Equip(_logicCharacter.Data.quipDict.ContainsValue(slot.dataOfSlot));
        }
    }

    public override void OnClick(int id)
    {
        base.OnClick(id);
        details.SetData(((InventorySlotUI)slotUIs[id]).dataOfSlot);
    }

}
