using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ConfigItem
{
    public int id = -1;
    public abstract void ApplyFromRow(IDictionary<string, object> row);
}


public interface ConfigBase
{
    string fileName { get; set; }

    void InitData(IList<IDictionary<string, object>> configs);

    void Clear();

}
