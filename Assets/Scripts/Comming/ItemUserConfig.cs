using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class ItemUserConfig : SingletonBase<ItemUserConfig>
{
    public List<ItemUserCfgItem> mDatas = new List<ItemUserCfgItem>();
    public Dictionary<int, ItemUserCfgItem> mCfgDict { get; private set; } = new Dictionary<int, ItemUserCfgItem>();

    public string fileName { get; set; } = typeof(ItemUserCfgItem).Name;

    // ------------------ Init ------------------
    public void Clear()
    {
        mDatas.Clear();
        mCfgDict.Clear();
    }

    public ItemUserCfgItem InitData()
    {
        Clear();

        if (!LoadJsonFromFile()) return null;

        if (mDatas == null || mDatas.Count == 0) return null;

        foreach (var row in mDatas)
        {
            if (row == null || row.id < 0) continue;
            row.Init();
            mCfgDict[row.id] = row;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(ItemUserCfgItem).Name} from JSON");
        return GetConfigItem(0);
    }

    public ItemUserCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }

    public IEnumerator<KeyValuePair<int, ItemUserCfgItem>> GetEnumerator()
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
            mDatas = JsonConvert.DeserializeObject<List<ItemUserCfgItem>>(json);
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
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        string json = JsonConvert.SerializeObject(mDatas, Formatting.Indented);

        await Task.Run(() => File.WriteAllText(path, json));
        Debug.Log($"JSON saved: {path}");
    }

    public void ExportToJson(List<ItemUserCfgItem> datas, string file)
    {
        if (datas == null) return;

        string path = Path.Combine(Application.persistentDataPath, file + ".json");
        string json = JsonConvert.SerializeObject(datas, Formatting.Indented);
        File.WriteAllText(path, json);

        Debug.Log($"ExportToJson: {path}");
    }
}
