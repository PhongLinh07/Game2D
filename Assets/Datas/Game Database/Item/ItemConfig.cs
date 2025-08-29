using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConfig : SingletonBase<ItemConfig>, ConfigBase
{
    public IDictionary<int, ItemCfgItem> mCfgDict { get; private set; } = new Dictionary<int, ItemCfgItem>();

    public string fileName { get; set; }


    public void InitData(IList<IDictionary<string, object>> configs) { }

    public void Clear() { }

    public void InitData(List<ItemCfgItem> datas)
    {
        mCfgDict.Clear();

        if (datas == null) return;

        foreach (var row in datas)
        {
            if (row == null || row.id < 0) continue;

            ItemCfgItem item = new ItemCfgItem();
            item.CopyFrom(row);
            mCfgDict[item.id] = item;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(ItemCfgItem).Name} from JSON");
    }

    public ItemCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }
}

