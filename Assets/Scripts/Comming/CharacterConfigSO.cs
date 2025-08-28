using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Character")]
public class CharacterConfigSO : ScriptableObject
{
    public List<ChacterCfgItem> datas;

    // Convert SO thành List<Dictionary<string, object>> (sử dụng JObject để giữ nguyên list và nested object)
    public IList<IDictionary<string, object>> ToDict()
    {
        if (datas == null || datas.Count == 0) return null;

        var listDict = new List<IDictionary<string, object>>();

        foreach (var data in datas)
        {
            if (data == null) continue;

            // Serialize toàn bộ object thành JObject
            var jObject = JObject.FromObject(data);

            // Convert JObject thành Dictionary<string, object>
            var dict = jObject.ToObject<Dictionary<string, object>>();

            listDict.Add(dict);
        }

        return listDict;
    }

    // Serialize List<Dictionary<string, object>> -> JSON string
    public string ToJson()
    {
        var dicts = ToDict();
        if (dicts == null) return "[]"; // Tránh null
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
