using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//========== SkillConfig (Quản lý toàn bộ bảng Skill) ==========
public class SkillConfig : SingletonBase<SkillConfig>, ConfigBase
{
    // Dictionary cho truy xuất O(1) theo id
    public IDictionary<int, SkillCfgItem> mCfgDict { get; private set; } = new Dictionary<int, SkillCfgItem>();

    // Tên file config (nếu hệ thống load của anh có dùng)
    public string fileName { get; set; } = "SkillConfig.json";

    /// <summary>
    /// Nạp dữ liệu từ bảng (mỗi phần tử trong configs là một "row" = Dictionary<string, object>).
    /// Ghi chú:
    /// - Nên gọi hàm này sau khi đã đọc và chuyển file Excel/CSV/JSON vào dạng IList<IDictionary<string, object>>.
    /// - Hàm sẽ clear dict cũ và build lại hoàn toàn.
    /// </summary>
    public void InitData(IList<IDictionary<string, object>> configs)
    {
        mCfgDict.Clear();
        if (configs == null) return;

        foreach (var row in configs)
        {
            if(row == null || !row.ContainsKey("id")) continue;

            var item = new SkillCfgItem();
            item.ApplyFromRow(row);

            // Bảo vệ: id < 0 thì bỏ qua để tránh đè key 0 vô nghĩa
            if (item.Id < 0) continue;

            mCfgDict[item.Id] = item;
        }
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
    public IEnumerator<KeyValuePair<int, SkillCfgItem>> GetEnumerator()
    {
        return mCfgDict.GetEnumerator();
    }

    /// <summary>
    /// Lấy nhanh 1 skill theo id. Trả null nếu không có.
    /// </summary>
    public SkillCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }
   
}

