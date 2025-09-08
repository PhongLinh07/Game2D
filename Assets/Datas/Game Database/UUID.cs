using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public enum EUUIDType
{
    ItemTemplate = 0,
    ItemInstance = 1,
    Player = 2

}


public class UUIDCfgItem : ConfigItem
{
    public EUUIDType uuidType;
    public int uuidCurrent = -1;

    public override void ApplyFromRow(IDictionary<string, object> row) { }
}

public class UUIDConfig: SingletonBase<UUIDConfig>
{
    public List<UUIDCfgItem> template = new List<UUIDCfgItem> // try
    {
        new UUIDCfgItem{uuidType = EUUIDType.ItemTemplate},
        new UUIDCfgItem{uuidType = EUUIDType.ItemInstance},
        new UUIDCfgItem{uuidType = EUUIDType.Player},
    };

    private List<UUIDCfgItem> mDatas = new List<UUIDCfgItem>();
    public IDictionary<EUUIDType, UUIDCfgItem> mCfgDict { get; private set; } = new Dictionary<EUUIDType, UUIDCfgItem>();

    public string fileName { get; set; } = typeof(UUIDCfgItem).Name;

    // ------------------ Init ------------------
    public void Clear()
    {
        mDatas.Clear();
        mCfgDict.Clear();
    }

    public bool InitData()
    {
        Clear();

        if (!LoadJsonFromFile()) return false;

        if (mDatas == null || mDatas.Count == 0) return false;

        foreach (var row in mDatas)
        {
            if (row == null || row.id < 0) continue;
           
            mCfgDict[row.uuidType] = row;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(UUIDCfgItem).Name} from JSON");
        return true;
    }

    public int GetUUID(EUUIDType uuidType)
    {
        return mCfgDict.TryGetValue(uuidType, out var uuid) ? uuid.uuidCurrent : -1;
    }
   
    public int GeneratorUUID(EUUIDType uuidType)
    {
        return mCfgDict.TryGetValue(uuidType, out var uuid) ? ++uuid.uuidCurrent : -1;
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
            mDatas = JsonConvert.DeserializeObject<List<UUIDCfgItem>>(json);
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
        if(mDatas == null || mDatas.Count == 0)
{
            Debug.LogWarning("Dữ liệu rỗng, không lưu JSON");
            return; // dừng luôn, không lưu
        }

        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        string json = JsonConvert.SerializeObject(mDatas, Formatting.Indented);

        await Task.Run(() => File.WriteAllText(path, json));

        Debug.Log($"JSON saved: {path}");
    }

    public void ExportToJson(List<UUIDCfgItem> datas, string file)
    {
        if (datas == null) return;

        string path = Path.Combine(Application.persistentDataPath, file + ".json");
        string json = JsonConvert.SerializeObject(datas, Formatting.Indented);
        File.WriteAllText(path, json);

        Debug.Log($"ExportToJson: {path}");
    }
}
