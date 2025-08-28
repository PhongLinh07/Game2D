using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//ItemConfig <=> ConfigMgr
public class ConfigMgr<T>: SingletonBase<ConfigMgr<T>>, ConfigBase where T : ConfigItem, new()
{
    // Dictionary cho truy xuất O(1) theo id
    public IDictionary<int, T> mCfgDict { get; private set; } = new Dictionary<int, T>();

    // Tên file config (nếu hệ thống load của anh có dùng)
    public string fileName { get; set; } = typeof(T).Name + ".json";

    /// <summary>
    /// Nạp dữ liệu từ bảng (mỗi phần tử trong configs là một "row" = Dictionary<string, object>).
    /// Ghi chú:
    /// - Nên gọi hàm này sau khi đã đọc và chuyển file Excel/CSV/JSON vào dạng IList<IDictionary<string, object>>.
    /// - Hàm sẽ clear dict cũ và build lại hoàn toàn.
    /// </summary>
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


        foreach (var row in dicts)
        {
            if (row == null || !row.ContainsKey("id")) continue;

            var item = new T();
            item.ApplyFromRow(row);

            // Bảo vệ: id < 0 thì bỏ qua để tránh đè key 0 vô nghĩa
            if (item.id < 0) continue;

            mCfgDict[item.id] = item;
        }

        Debug.Log($"Loaded {mCfgDict.Count} {typeof(T).Name} from JSON");
    }

    /// <summary>
    /// Dọn sạch bộ nhớ tạm của config (thường gọi khi đổi scene / reload).
    /// </summary>
    public void Clear()
    {
        mCfgDict.Clear();
    }

    /// <summary>
    /// Cho phép foreach(SkillConfig.Instance) ... nếu anh muốn.
    /// </summary>
    public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
    {
        return mCfgDict.GetEnumerator();
    }

    /// <summary>
    /// Lấy nhanh 1 skill theo id. Trả null nếu không có.
    /// </summary>
    public T GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }
}
