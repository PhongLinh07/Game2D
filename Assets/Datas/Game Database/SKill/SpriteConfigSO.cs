using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Atlas")]
public class SpriteConfigSO : ScriptableObject
{
    public List<Texture2D> datas;

    public IList<IDictionary<string, object>> ToDict()
    {
        List<IDictionary<string, object>> dict = new List<IDictionary<string, object>>();

        if (datas == null || datas.Count == 0) return null;

        foreach (var data in datas)
        {
            if (data == null) continue;

            var row = new Dictionary<string, object>
            {
                ["sprite"] = data != null ? data.name : null
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
