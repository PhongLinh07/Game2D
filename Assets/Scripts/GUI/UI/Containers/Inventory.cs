using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Inventory : AContainer<EnhanceCfgItem>
{
    public DetailsItem details;

    public PlayerData player;

    public override void Init()
    {
        slotUIs.Clear();

        for (int i = 0; i < player.items.Count; i++)
        {
            EnhanceCfgItem cpy = new EnhanceCfgItem();
            cpy.CopyFrom(player.items[i]);
            cpy.Data = ItemConfig.GetInstance.GetConfigItem(cpy.Data.Id);
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
