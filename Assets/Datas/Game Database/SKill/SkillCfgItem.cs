using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.U2D;


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

    [SerializeField] public List<Attribute> attributes = new();
    [JsonIgnore] public Dictionary<EAttribute, float> attrDict = new Dictionary<EAttribute, float>();

    public EHeroSkillType ESkillLg;
    public ASkillLogic Logic;
    public SkillInputType InputType;
    public EWeaponType weaponType;

    public override void ApplyFromRow(IDictionary<string, object> row) { }

    internal void CopyFrom(SkillCfgItem other)
    {
        id = other.id;
        Name = other.Name;
        Icon = other.Icon;
        Description = other.Description;
        attributes = other.attributes;
        ESkillLg = other.ESkillLg;
        Logic = other.Logic;
        InputType = other.InputType;
        weaponType = other.weaponType;

        Init();
    }

    public void Init()
    {
        foreach (var attr in attributes)
        {
            attrDict[attr.attribute] = attr.value;
        }
    }
    public override string ToString()
    {
        string des;
 

        des = $"{Description}\n";

        foreach (var row in attrDict)
        {
            des += $"{row.Key.ToString()}: {row.Value}\n";
        }

        return des;
    }


}

/*
 * Item -> ItemUSer
 * Skill ->SkillUser
 * 
 */