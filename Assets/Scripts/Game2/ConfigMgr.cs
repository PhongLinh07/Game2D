using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

//ItemConfig <=> ConfigMgr
public class ConfigMgr<T>: SingletonBase<ConfigMgr<T>>, ConfigBase where T : ConfigItem, new()
{
    private List<T> mDatas = new();
    public IDictionary<int, T> mCfgDict { get; private set; } = new Dictionary<int, T>();

    [System.Serializable]
    private class Wrapper
    {
        public List<T> datas;
        public Wrapper(List<T> list) { datas = list; }
    }
     
    public string fileName { get; set; } = typeof(T).Name + ".json";


    public void InitData(IList<IDictionary<string, object>> configs) { }
   

    public void InitData()
    {
        mDatas.Clear();
        mCfgDict.Clear();

        LoadJsonFromFile();

        if (mDatas == null) return;

        foreach (var row in mDatas)
        {
            if (row == null || row.id < 0) continue;

            mCfgDict[row.id] = row;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(T).Name} from JSON");
    }

   
    public void Clear()
    {
        mDatas.Clear();
        mCfgDict.Clear();
    }

    
    public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
    {
        return mCfgDict.GetEnumerator();
    }

    
    public T GetConfigItem(int id)
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
        string path = Path.Combine(Application.persistentDataPath, fileName);
        await Task.Run(() => File.WriteAllText(path, ToJson()));
        Debug.Log($"JSON saved: {path}");
    }

    private void LoadJsonFromFile()
    {

        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path)) return;
        string json = File.ReadAllText(path);
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        mDatas = wrapper.datas;
        Debug.Log($"JSON loaded: {path}");

    }

    public void ExportToJson(List<T> datas, string file)
    {
        if (datas == null) return;

        string path = Path.Combine(Application.persistentDataPath, file + ".json");

        File.WriteAllText(path, JsonUtility.ToJson(new Wrapper(datas), true));

        Debug.Log($"JSON saved: {path}");

    }

}
