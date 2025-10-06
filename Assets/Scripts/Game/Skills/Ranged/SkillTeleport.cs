using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTeleport : ASkillLogic
{
    private SkillCfgItem data;
    private Vector2 posCast;

    // Start is called before the first frame updateS
    public void Init(SkillCfgItem skill)
    {
        data = skill;
    }

    public override IEnumerator Cast(IndicatorData indicator)
    {
        posCast = (Vector2)indicator.Position;

        Teleport();
        yield return null;
    }


    void Teleport()
    {
        LogicCharacter.Instance.UseSkill(data.id);//try
        LogicCharacter.Instance.Teleport(posCast);
    }
}
