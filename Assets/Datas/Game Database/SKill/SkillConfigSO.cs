using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[CreateAssetMenu(menuName = "DataBase/Skill")]
public class SkillConfigSO : ScriptableObject
{
    public List<SkillCfgItem> datas;

    public void LoadData()
    {
        SkillConfig.GetInstance.InitData(datas);
    }
}
