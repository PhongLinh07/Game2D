using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public enum ItemRarity
{
    Mortal = 0,       // Phàm phẩm
    Genuine = 1,      // Chân phẩm
    Superior = 2,     // Thượng phẩm
    Celestial = 3,    // Tiên phẩm
    Divine = 4        // Thần phẩm
}

[System.Serializable]
public class ItemCfgItem : ConfigItem
{
    public int Id;
    public string Name;
    public bool Stackable;
    public Sprite Icon;
    public string Description;

    internal void ApplyFromRow(IDictionary<string, object> row)
    {
        Id = row.ContainsKey("id") ? Convert.ToInt32(row["id"]) : -1;
        Name = row.ContainsKey("name") ? row["name"]?.ToString() : null;
        Stackable = row.ContainsKey("stackable") ? (bool)row["stackable"] : false;
        Icon = SpriteConfig.GetInstance.GetSprite(null, row["icon"].ToString());
        Description = row.ContainsKey("desc") ? row["desc"]?.ToString() : null;
       
    }

    internal void CopyFrom(ItemCfgItem other)
    {
        Id = other.Id;
        Name = other.Name;
        Stackable = other.Stackable;
        Icon = other.Icon;
        Description = other.Description;
    }
}


public class ItemConfig : SingletonBase<ItemConfig>, ConfigBase
{
    // Dictionary cho truy xuất O(1) theo id
    public IDictionary<int, ItemCfgItem> mCfgDict { get; private set; } = new Dictionary<int, ItemCfgItem>();

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
            if (row == null || !row.ContainsKey("id")) continue;

            var item = new ItemCfgItem();
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
    public IEnumerator<KeyValuePair<int, ItemCfgItem>> GetEnumerator()
    {
        return mCfgDict.GetEnumerator();
    }

    /// <summary>
    /// Lấy nhanh 1 skill theo id. Trả null nếu không có.
    /// </summary>
    public ItemCfgItem GetConfigItem(int id)
    {
        return mCfgDict.TryGetValue(id, out var item) ? item : null;
    }
}


[System.Serializable]
public class EnhanceCfgItem : ConfigItem
{
    public int Id;
    public ItemCfgItem Data;     
    public int Level;
    public ItemRarity Rarity;
    public int Quantity;
    public int Atk;
    public int Def;


    internal void CopyFrom(EnhanceCfgItem other)
    {
        Data = other.Data;
        Id = other.Id;
        Rarity = other.Rarity;
        Level = other.Level;
        Quantity = Data.Stackable ? other.Quantity : 1;
        Atk = other.Atk;
        Def = other.Def;
    }

    public string GetDescription()
    {
        string des;
        
        if(Data.Stackable)
        {
            des = $"{Data.Description}\n";
        }
        else
        {
            des =
                $"Level: {Level}\n" +
                $"{Data.Description}\n" +
                $"Atk: {Atk}\n" +
                $"Def: {Def}\n";
        }  
        return des;
    }
}


