using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Skill")]
public class SkillConfigSO : ScriptableObject
{
    public List<SkillCfgItem> datas;

    public IList<IDictionary<string, object>> ToDict()
    {
        List<IDictionary<string, object>> dict = new List<IDictionary<string, object>>();

        if (datas == null || datas.Count == 0) return null;

        foreach (var data in datas)
        {
            if (data == null) continue;

            var row = new Dictionary<string, object>
            {
                ["id"] = data.Id,
                ["name"] = data.Name,
                ["icon"] = data.Icon != null ? data.Icon.name : null,
                ["desc"] = data.Description,
                ["atk"] = data.atk,
                ["cooldown"] = data.cooldown,
                ["eSkillLg"] = data.ESkillLg,
                ["Logic"] = data.Logic,
                ["inputType"] = data.InputType
            };

            dict.Add(row);
        }

        return dict;
    }
}
