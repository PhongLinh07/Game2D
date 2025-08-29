using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : AContainer<EnhanceCfgItem>
{
    public DetailsItem details;
  

    public override void Init()
    {
        slotUIs.Clear();

        foreach(var item in CharacterConfig.GetInstance.GetConfigItem(0).items)
        {
            EnhanceCfgItem cpy = new EnhanceCfgItem();
            cpy.CopyFrom(item);
            datas.Add(cpy);
        }

        base.Init();
    }
    public override void OnClick(int id)
    {
        base.OnClick(id);
        details.SetData(((ItemSlotUI)slotUIs[id]).dataOfSlot);
    }

}
