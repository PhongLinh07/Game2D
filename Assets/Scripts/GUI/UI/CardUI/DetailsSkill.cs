using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailsSkill : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDescription;

    // Start is called before the first frame updatehuws trong scrip 
    public void SetData(SkillCfgItem skill)
    {
        icon.sprite = skill.Icon;
        skillName.text = skill.Name;
        skillDescription.text = skill.Description;
    }
}
