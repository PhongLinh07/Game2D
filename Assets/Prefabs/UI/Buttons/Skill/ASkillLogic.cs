using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public abstract class ASkillLogic
{
    public static ASkillLogic GetLogic(SkillCfgSkill skill)
    {
        switch (skill.ESkillLg)
        {
            case ESkillLogic.FallingSword:
                {
                    CastFallingSword newLogic = new CastFallingSword();
                    newLogic.Init(skill);
                    skill.Logic = newLogic;
                    return newLogic;
                }

            case ESkillLogic.SpreadSword:
                {
                    SpreadShot newLogic = new SpreadShot();
                    newLogic.Init(skill);
                    skill.Logic = newLogic;
                    return newLogic;
                }
        }

        return null;
    }

    public string tagOfTarget;
    public SkillCfgSkill owner;

    public abstract IEnumerator Cast(params object[] args);
}

