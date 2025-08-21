using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : AContainer<PlayerItem>
{
    public DetailsItem details;

    public PlayerData player;

    public override void Init()
    {
        slotUIs.Clear();

        for (int i = 0; i < player.playerItems.Count; i++)
        {
            PlayerItem cpy = new PlayerItem();
            cpy.CopyFrom(player.playerItems[i]);
            cpy.Data = GameManager.Instance.DB_Item.GetById(cpy.Data.Id);
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
