using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterConfig : SingletonBase<CharacterConfig>
{
    private List<CharacterCfgItem> mDatas = new List<CharacterCfgItem>();
    public IDictionary<int, CharacterCfgItem> mCfgDict { get; private set; } = new Dictionary<int, CharacterCfgItem>();

    public string fileName { get; set; } = typeof(CharacterCfgItem).Name;

    // ------------------ Init ------------------
    public void Clear()
    {
        mDatas.Clear();
        mCfgDict.Clear();
    }

    public CharacterCfgItem InitData()
    {
        Clear();

        if (!LoadJsonFromFile()) return null;

        if (mDatas == null || mDatas.Count == 0) return null;

        foreach (var row in mDatas)
        {
            if (row == null || row.id < 0) continue;
            mCfgDict[row.id] = row;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(CharacterConfig).Name} from JSON");
        return GetConfigItem(0);
    }

    public CharacterCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }

    public IEnumerator<KeyValuePair<int, CharacterCfgItem>> GetEnumerator()
    {
        return mCfgDict.GetEnumerator();
    }

    // ------------------ Load / Save JSON ------------------
    private bool LoadJsonFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        if (!File.Exists(path))
        {
            Debug.LogWarning($"JSON file not found: {path}");
            return false;
        }

        string json = File.ReadAllText(path);

        try
        {
            mDatas = JsonConvert.DeserializeObject<List<CharacterCfgItem>>(json);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"JSON parse error: {ex.Message}");
            return false;
        }
    }

    public async Task SaveJsonAsync()
    {
        if (mDatas == null || mDatas.Count == 0)
        {
            Debug.LogWarning("Dữ liệu rỗng, không lưu JSON");
            return; // dừng luôn, không lưu
        }
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        string json = JsonConvert.SerializeObject(mDatas, Formatting.Indented);

        await Task.Run(() => File.WriteAllText(path, json));
        Debug.Log($"JSON saved: {path}");
    }

    public void ExportToJson(List<CharacterCfgItem> datas, string file)
    {
        if (datas == null) return;

        string path = Path.Combine(Application.persistentDataPath, file + ".json");
        string json = JsonConvert.SerializeObject(datas, Formatting.Indented);
        File.WriteAllText(path, json);

        Debug.Log($"ExportToJson: {path}");
    }
}
