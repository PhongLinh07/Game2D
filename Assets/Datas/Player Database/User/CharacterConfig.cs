using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

//ItemConfig <=> ConfigMgr
public class CharacterConfig : SingletonBase<CharacterConfig>, ConfigBase
{
    private List<ChacterCfgItem> mDatas = new();
    public IDictionary<int, ChacterCfgItem> mCfgDict { get; private set; } = new Dictionary<int, ChacterCfgItem>();

    [System.Serializable]
    private class Wrapper
    {
        public List<ChacterCfgItem> datas;
        public Wrapper(List<ChacterCfgItem> list) { datas = list; }
    }

    public string fileName { get; set; } = typeof(ChacterCfgItem).Name;


    public void InitData(IList<IDictionary<string, object>> configs) { }


    public ChacterCfgItem InitData()
    {
        mDatas.Clear();
        mCfgDict.Clear();

        LoadJsonFromFile();

        if (mDatas == null) return null;

        foreach (var row in mDatas)
        {
            if (row == null || row.id < 0) continue;

            mCfgDict[row.id] = row;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(CharacterConfig).Name} from JSON");
        return mCfgDict[0];
    }


    public void Clear()
    {
        mDatas.Clear();
        mCfgDict.Clear();
    }


    public IEnumerator<KeyValuePair<int, ChacterCfgItem>> GetEnumerator()
    {
        return mCfgDict.GetEnumerator();
    }


    public ChacterCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }






















    private string ToJson()
    {
        if (mDatas == null || mDatas.Count == 0)
            return "{}";

        return JsonUtility.ToJson(new Wrapper(mDatas), true);
    }

    // Save JSON -> File
    public async Task SaveJsonAsync()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        await Task.Run(() => File.WriteAllText(path, ToJson()));
        Debug.Log($"JSON saved: {path}");
    }

    private void LoadJsonFromFile()
    {

        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        mDatas = wrapper.datas;
        Debug.Log($"JSON loaded: {path}");

    }

    public void ExportToJson(List<ChacterCfgItem> datas, string file)
    {
        if (datas == null) return;

        string path = Path.Combine(Application.persistentDataPath, file + ".json");

        File.WriteAllText(path, JsonUtility.ToJson(new Wrapper(datas), true));

        Debug.Log($"JSON saved: {path}");

    }

}
