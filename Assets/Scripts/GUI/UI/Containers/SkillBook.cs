using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class SkillBook : AContainer<SkillCfgItem>
{  
    public DetailsSkill details;
  

    public override void Init()
    {
        slotUIs.Clear();

        for (int i = 0; i < GameManager.Instance.R_PlayerData.skillsLearned.Count; i++)
        {
            SkillCfgItem cpy = new SkillCfgItem();
            cpy.CopyFrom(ConfigMgr<SkillCfgItem>.GetInstance.GetConfigItem(GameManager.Instance.R_PlayerData.skillsLearned[i]));
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
            index = GameManager.Instance.R_PlayerData.skillsEquipped.IndexOf(datas[i].id);
            ((SkillSlotUI)slotUIs[i]).Equip(index == -1 ? false : true);
        }
    }
    public override void OnClick(int id) 
    {
        base.OnClick(id);
        details.SetData(((SkillSlotUI)slotUIs[id]).dataOfSlot);
    }

}
