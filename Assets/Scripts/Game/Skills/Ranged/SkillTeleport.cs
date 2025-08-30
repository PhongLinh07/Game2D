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

    public override IEnumerator Cast(params object[] args)
    {
        posCast = (Vector2)args[0]; // ép kiểu thủ công

        Teleport();
        yield return null;
    }


    void Teleport()
    {
        LogicCharacter.Instance.Teleport(posCast);
    }
}
