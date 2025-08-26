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

        for (int i = 0; i < GameManager.Instance.R_PlayerData.items.Count; i++)
        {
            EnhanceCfgItem cpy = new EnhanceCfgItem();
            cpy.CopyFrom(GameManager.Instance.R_PlayerData.items[i]);
            cpy.Data = ConfigMgr<ItemCfgItem>.GetInstance.GetConfigItem(cpy.Data.id);
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
