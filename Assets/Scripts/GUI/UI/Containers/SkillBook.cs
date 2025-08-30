using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class SkillBook : AContainer<SkillCfgItem>
{  
    public DetailsSkill details;

    [SerializeField]
    private LogicCharacter logicCharacter;

    public override void Init()
    {
        logicCharacter = LogicCharacter.Instance;
        slotUIs.Clear();

        foreach(int id in logicCharacter.Data.SkillsLearned)
        {
            SkillCfgItem cpy = new SkillCfgItem();
            cpy.CopyFrom(SkillConfig.GetInstance.GetConfigItem(logicCharacter.Data.SkillsLearned[id]));
            datas.Add(cpy);
      
        }

        base.Init();
    }

    public override void UpdateContainer()
    {
        base.UpdateContainer();

        // Cập nhật vào UI
        foreach(int id in logicCharacter.Data.SkillsLearned)
        {
            ((SkillSlotUI)slotUIs[id]).Equip(logicCharacter.Data.SkillsEquipped.Contains(id));
        }
    }
    public override void OnClick(int id) 
    {
        base.OnClick(id);
        details.SetData(((SkillSlotUI)slotUIs[id]).dataOfSlot);
    }

}
