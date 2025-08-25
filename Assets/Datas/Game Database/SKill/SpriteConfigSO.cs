using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Atlas")]
public class SpriteConfigSO : ScriptableObject
{
    public List<SpriteCfgItem> datas;

    public IList<IDictionary<string, object>> ToDict()
    {
        List<IDictionary<string, object>> dict = new List<IDictionary<string, object>>();

        if (datas == null || datas.Count == 0) return null;

        foreach (var data in datas)
        {
            if (data == null) continue;

            var row = new Dictionary<string, object>
            {
                ["id"] = data.id,
                ["atlas"] = data.atlas,
                ["sprite"] = data.sprite
            };

            dict.Add(row);
        }

        return dict;
    }
}
