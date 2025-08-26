using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[CreateAssetMenu(menuName = "DataBase/Skill")]
public class SkillConfigSO : ScriptableObject
{
    public List<SkillCfgItem> datas;

    // Convert SO thành List<Dictionary<string, object>>
    public IList<IDictionary<string, object>> ToDict()
    {
        List<IDictionary<string, object>> dict = new List<IDictionary<string, object>>();

        if (datas == null || datas.Count == 0) return null;

        foreach (var data in datas)
        {
            if (data == null) continue;

            var row = new Dictionary<string, object>
            {
                ["id"] = data.id,
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

    // Serialize Dictionary -> JSON string
    public string ToJson()
    {
        var dicts = ToDict();
        return JsonConvert.SerializeObject(dicts, Formatting.Indented);

    }

    // Save JSON -> File
    public void SaveJsonToFile(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        string json = ToJson();
        File.WriteAllText(path, json);
        Debug.Log($"JSON saved: {path}");
    }
}
