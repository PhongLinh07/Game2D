using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillLogic
{
    None = -1,
    BaseAttack = 0,
    SpreadSword = 1,
    FallingSword = 2,
    Teleport = 3,
}


[System.Serializable]
public class SkillCfgSkill : ConfigItem<SkillCfgSkill>
{
    public string Name;
    public Sprite Icon;
    public string Description;
    public int atk;
    public float cooldown;
    public ESkillLogic ESkillLg;
    public ASkillLogic Logic;
    public SkillInputType InputType;

    public override void CopyFrom(SkillCfgSkill skill)
    {
        Id = skill.Id;
        Name = skill.Name;
        Icon = skill.Icon;
        Description = skill.Description;
        atk = skill.atk;
        cooldown = skill.cooldown;
        ESkillLg = skill.ESkillLg;
        Logic = skill.Logic;
        InputType = skill.InputType;
    }
}



[CreateAssetMenu(menuName = "DataBase/Skill")]
public class SkillConfig : ConfigBase<SkillCfgSkill> { }
