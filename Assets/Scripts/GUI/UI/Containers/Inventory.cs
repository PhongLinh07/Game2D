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

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }

    private void Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;
        _logicCharacter = logicCharacter;

    }

    public override void Init()
    {
        _logicCharacter = LogicCharacter.Instance;
        slotUIs.Clear();

        foreach(var item in _logicCharacter.Data.items)
        {
            InventorySlotUI newSlot = Instantiate(slotPrefab, parent).GetComponent<InventorySlotUI>();
            newSlot.SetData<ItemUserCfgItem>(item.Value);
            slotUIs[item.Key] = newSlot;

        }
        
        isInitialized = true;

    }

    public void EquipItem(ItemUserCfgItem item)
    {
        ItemCfgItem itemCfg = item.GetTemplate();

        if(itemCfg.equipType == EEquipmentType.None) return;

        if (!_logicCharacter.Data.quipDict.ContainsKey(itemCfg.equipType)) // nếu key chưa tồn tại
        {
            _logicCharacter.Equipment(itemCfg.equipType, item);
          
        }
        else // nếu key đã tồn tại
        {
            if(_logicCharacter.Data.quipDict[itemCfg.equipType].id == item.id) // nếu là lần 2 thì đảo ngược
            {
                Debug.LogWarning("sdfsdfsdfssfsfgsgfs");
                _logicCharacter.Unequipment(itemCfg.equipType); 
            }
            else
            {
                Debug.LogWarning("??????????????????????//");

                _logicCharacter.Equipment(itemCfg.equipType, item);
            }

                
        }

        UpdateContainer();
    }
    public override void UpdateContainer()
    {
        InventorySlotUI s;
        ItemUserCfgItem i;
        foreach (var slot in slotUIs)
        {
            s = (InventorySlotUI)slot.Value;
            i = s.dataOfSlot;

            if(_logicCharacter.Data.quipDict.ContainsKey(i.GetTemplate().equipType))
            {
                s.Equip(_logicCharacter.Data.quipDict[i.GetTemplate().equipType].id == i.id);
            }
            else
            {
                s.Equip(false);
            }
            
            
        }
    }

    public override void OnClick(int id)
    {
        base.OnClick(id);
        details.SetData(((InventorySlotUI)slotUIs[id]).dataOfSlot);
    }

}
