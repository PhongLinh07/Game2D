using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : AContainer<ItemUseCfgItem>
{
    public DetailsItem details;

    public static Inventory Instance;

    private LogicCharacter _logicCharacter;


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

        foreach(var item in _logicCharacter.Data.itemDict)
        {
            ItemUseCfgItem cpy = new ItemUseCfgItem();
            cpy.CopyFrom(item.Value);
            datas.Add(cpy);
        }

        base.Init();
    }

    public bool EquipItem(ItemUseCfgItem item)
    {
        ItemCfgItem itemCfg = ItemConfig.GetInstance.GetConfigItem(item.idItem);

        if(itemCfg.equipType == EEquipType.None) return false;

        if (!_logicCharacter.Data.quipDict.ContainsKey(itemCfg.equipType)) // nếu key chưa tồn tại
        {
            _logicCharacter.Equipment(itemCfg.equipType, item.id);
            return true;

        }
        else
        {
            if(_logicCharacter.Data.quipDict[itemCfg.equipType] == item.id) // nếu là lần 2 thì đảo ngược
            {
                _logicCharacter.Unequipment(itemCfg.equipType);
                return false;
            }

            ((InventorySlotUI)slotUIs[_logicCharacter.Data.quipDict[itemCfg.equipType]]).Equip(false); // giải phóng item trước đó

            _logicCharacter.Equipment(itemCfg.equipType, item.id); // Equipment
            return true;
        }


    }

    public override void OnClick(int id)
    {
        base.OnClick(id);
        details.SetData(((InventorySlotUI)slotUIs[id]).dataOfSlot);
    }

}
