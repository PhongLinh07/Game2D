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

        foreach(int id in ConfigMgr<ChacterCfgItem>.GetInstance.GetConfigItem(0).SkillsLearned)
        {
            SkillCfgItem cpy = new SkillCfgItem();
            cpy.CopyFrom(ConfigMgr<SkillCfgItem>.GetInstance.GetConfigItem(ConfigMgr<ChacterCfgItem>.GetInstance.GetConfigItem(0).SkillsLearned[id]));
            datas.Add(cpy);
      
        }

        base.Init();
    }

    public override void UpdateContainer()
    {
        base.UpdateContainer();

        // Cập nhật vào UI
        foreach(int id in ConfigMgr<ChacterCfgItem>.GetInstance.GetConfigItem(0).SkillsLearned)
        {
            ((SkillSlotUI)slotUIs[id]).Equip(ConfigMgr<ChacterCfgItem>.GetInstance.GetConfigItem(0).SkillsEquipped.Contains(id));
        }
    }
    public override void OnClick(int id) 
    {
        base.OnClick(id);
        details.SetData(((SkillSlotUI)slotUIs[id]).dataOfSlot);
    }

}
