using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Progress;

public class SpriteConfig : SingletonBase<SpriteConfig>, ConfigBase
{
    public string fileName { get; set; } = typeof(SpriteCfgItem).Name + ".json";

    private Dictionary<string, SpriteCfgItem> mCfgDict = new Dictionary<string, SpriteCfgItem>();

    public void InitData(IList<IDictionary<string, object>> configs) { }

    public void InitData()
    {
        mCfgDict.Clear();

        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogError("JSON file not found: " + path);
            return;
        }

        string json = File.ReadAllText(path);

        // Deserialize thành List<Dictionary<string, object>>
        var dicts = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

        foreach (var dic in dicts)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Art/" + dic["sprite"].ToString());
            Debug.Log("Art/" + dic["sprite"].ToString());
            foreach (var s in sprites)
            {
                var item = new SpriteCfgItem
                {
                    sprite = s
                };
                mCfgDict[s.name] = item;
            }

        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(SpriteCfgItem).Name} from JSON");
    }


    public void Clear()
    {
        mCfgDict.Clear();
    }

    public IEnumerator<KeyValuePair<int, SpriteCfgItem>> GetEnumerator()
    {
        return null;
    }

    public SpriteCfgItem GetConfigItem(string id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }

    // Lấy sprite theo tên sheet + sprite
    public Sprite GetSprite(string spriteName)
    {
        mCfgDict.TryGetValue(spriteName, out var item);

        if (!item.sprite) Debug.Log(spriteName);
  
        return item.sprite;
    }
}
