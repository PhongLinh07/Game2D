using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Progress;

public enum EHeroSkillType
{
    None = -1,
    Normal = 0,
    SpreadSword = 1,
    FallingSword = 2,
    Teleport = 3,
}

//========== SkillCfgItem (Một dòng skill trong bảng) ==========

[System.Serializable]
public class SkillCfgItem : ConfigItem
{
    public string Name;
    public Sprite Icon;
    public string Description;
    public int atk;
    public float cooldown;
    public EHeroSkillType ESkillLg;
    public ASkillLogic Logic;
    public SkillInputType InputType;
    public override void ApplyFromRow(IDictionary<string, object> row)
    {
        id              = row.ContainsKey("id") ? Convert.ToInt32(row["id"]) : -1;
        Name            = row.ContainsKey("name") ? row["name"]?.ToString() : null;

        Icon            = SpriteConfig.GetInstance.GetSprite(row["icon"].ToString());

        Description     = row.ContainsKey("desc") ? row["desc"]?.ToString() : null;
        atk             = row.ContainsKey("atk") ? Convert.ToInt32(row["atk"]) : 0;
        cooldown        = row.ContainsKey("cooldown") ? Convert.ToSingle(row["cooldown"]) : 0f;
        ESkillLg        = row.ContainsKey("eSkillLg") ? (EHeroSkillType)Convert.ToInt32(row["eSkillLg"]) : 0;
        InputType       = row.ContainsKey("inputType") ? (SkillInputType)Convert.ToInt32(row["inputType"]) : 0;
    }


    internal void CopyFrom(SkillCfgItem other)
    {
        id = other.id;
        Name = other.Name;
        Icon = other.Icon;
        Description = other.Description;
        atk = other.atk;
        cooldown = other.cooldown;
        ESkillLg = other.ESkillLg;
        Logic = other.Logic;
        InputType = other.InputType;
    }
    public override string ToString()
    {
        return "";
    }


}
