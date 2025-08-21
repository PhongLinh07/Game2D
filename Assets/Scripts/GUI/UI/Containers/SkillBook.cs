using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class SkillBook : AContainer<SkillCfgSkill>
{  
    public DetailsSkill details;
    public PlayerData player;
  

    public override void Init()
    {
        slotUIs.Clear();

        for (int i = 0; i < player.skills.Count; i++)
        {
            SkillCfgSkill cpy = new SkillCfgSkill();
            cpy.CopyFrom(GameManager.Instance.DB_Skill.GetById(player.skills[i]));
            datas.Add(cpy);
      
        }

        base.Init();
    }

    public override void UpdateContainer()
    {
        base.UpdateContainer();

        int index;
        // Cập nhật vào UI
        for (int i = 0; i < datas.Count; i++)
        {
            index = playerData.skillEquipped.IndexOf(datas[i].Id);
            ((SkillSlotUI)slotUIs[i]).Equip(index == -1 ? false : true);
        }
    }
    public override void OnClick(int id) 
    {
        base.OnClick(id);
        details.SetData(((SkillSlotUI)slotUIs[id]).dataOfSlot);
    }

}
