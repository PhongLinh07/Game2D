using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

public enum EItemType
{
    None = 0,
    Consumable = 1,  // 1. Thuốc/Item dùng một lần (HP potion, MP potion, buff tạm thời)
    Material = 2,    // 2. Nguyên liệu, vật phẩm không dùng trực tiếp (thảo dược, quest item)
    Armor = 3,       // 3. Trang bị phòng thủ (giáp, mũ, găng tay...)
    Weapon = 4      // 4. Vũ khí (kiếm, cung, quyền trượng...)
};

[System.Serializable]
public class ItemCfgItem : ConfigItem //Template
{
    public string Name;
    public bool Stackable;
    public Sprite Icon;
    public string Description;
    public EItemType itemType;
    public EEquipmentType equipType;
    public EWeaponType weaponType;
    [SerializeField] public List<Attribute> attributes = new();
    public override void ApplyFromRow(IDictionary<string, object> row) { }

    internal void CopyFrom(ItemCfgItem other)
    {
        id = other.id;
        Name = other.Name;
        Stackable = other.Stackable;
        Icon = other.Icon;
        Description = other.Description;
        itemType = other.itemType;
        equipType = other.equipType;
        weaponType = other.weaponType;
        attributes = other.attributes;
    }
}

[System.Serializable]
public class Attribute
{
    public EAttribute attribute;
    public float value;
}

