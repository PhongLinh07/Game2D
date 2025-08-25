using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ConfigItem
{
}

[System.Serializable]
public abstract class ConfigItem<T> // Data
{
    public int Id;
    public abstract void CopyFrom(T other); // clone dữ liệu từ other

}

public class ConfigBase<T> : ScriptableObject where T : ConfigItem<T>, new() //Database
{
    [SerializeField]
    public List<T> datas = new List<T>();

    public List<T> GetRuntimeCopy()
    {
        List<T> copyList = new List<T>();

        foreach (var s in datas)
        {
            T copy = new T(); // được phép vì có "new()" constraint
            copy.CopyFrom(s);
            copyList.Add(copy);
        }

        return copyList;
    }
}

public interface ConfigBase
{
    string fileName { get; set; }

    void InitData(IList<IDictionary<string, object>> configs);

    void Clear();
}
