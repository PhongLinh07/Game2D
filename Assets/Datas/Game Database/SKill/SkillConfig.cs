using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillConfig : SingletonBase<SkillConfig>, ConfigBase
{
    public IDictionary<int, SkillCfgItem> mCfgDict { get; private set; } = new Dictionary<int, SkillCfgItem>();

    public string fileName { get; set; }


    public void InitData(IList<IDictionary<string, object>> configs) { }

    public void Clear() { }

    public void InitData(List<SkillCfgItem> datas)
    {
        mCfgDict.Clear();

        if (datas == null) return;

        foreach (var row in datas)
        {
            if (row == null || row.id < 0) continue;

            SkillCfgItem skill = new SkillCfgItem();
            skill.CopyFrom(row);
            mCfgDict[skill.id] = skill;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(SkillCfgItem).Name} from JSON");
    }

    public SkillCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }
}

