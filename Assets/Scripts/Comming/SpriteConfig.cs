using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteConfig : SingletonBase<SpriteConfig>, ConfigBase
{
    public string fileName
    {
        get
        {
            return null;
        }
        set
        {
        }
    }

    private Dictionary<string, Sprite> mSpriteCache = new Dictionary<string, Sprite>();
    private Dictionary<int, SpriteCfgItem> mCfgDict = new Dictionary<int, SpriteCfgItem>();

    public void InitData(IList<IDictionary<string, object>> configs)
    {
        mCfgDict.Clear();

        foreach (var dic in configs)
        {
            var item = new SpriteCfgItem
            {
                id = (int)dic["id"],          
                atlas = dic["atlas"].ToString(),
                sprite = dic["sprite"].ToString()
            };
            mCfgDict[item.id] = item;



            Sprite[] sprites = Resources.LoadAll<Sprite>(item.atlas);
            foreach (var s in sprites)
            {
                mSpriteCache[s.name] = s;
            }
            
        }
    }
    

    public void Clear()
    {
        mCfgDict.Clear();
    }

    public IEnumerator<KeyValuePair<int, SpriteCfgItem>> GetEnumerator()
    {
        return null;
    }

    public SpriteCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }

    // Lấy sprite theo tên sheet + sprite
    public Sprite GetSprite(string spriteName, string sheetName)
    {
        mSpriteCache.TryGetValue(sheetName, out var sprite);

        if (!sprite) Debug.Log(sheetName);
  
        return sprite;
    }
}
