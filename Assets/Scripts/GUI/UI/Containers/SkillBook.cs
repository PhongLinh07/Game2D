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

        foreach(var skill in logicCharacter.Data.SkillsLearnedDict)
        {   
            datas.Add(skill.Value);
        }

        base.Init();
    }

    
    public override void UpdateContainer()
    {
        SkillSlotUI s;
        SkillCfgItem i;
        foreach (var slot in slotUIs)
        {
            s = (SkillSlotUI)slot.Value;
            i = s.dataOfSlot;

            s.Equip(logicCharacter.Data.SkillsEquippedDict.ContainsKey(i.id));
        }
    }
    public override void OnClick(int id) 
    {
        base.OnClick(id);
        details.SetData(((SkillSlotUI)slotUIs[id]).dataOfSlot);
    }

}
