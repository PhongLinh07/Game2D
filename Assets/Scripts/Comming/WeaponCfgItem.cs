using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[System.Serializable]
public enum EWeaponType
{
    None = 0,
    Sword = 1,      // Kiếm
    Bow = 2,        // Cung
    Staff = 3,      // Trượng/phép thuật
    Axe = 4,        // Rìu
    Spear = 5,      // Thương
    Dagger = 6,     // Dao găm
    Hammer = 7,     // Búa
    Flag = 8,       // Cờ

}


public class WeaponCfgItem : ConfigItem
{
    public EWeaponType weaponType;
    public GameObject prefab;

    public override void ApplyFromRow(IDictionary<string, object> row) { }

    internal void CopyFrom(WeaponCfgItem other)
    {
        id = other.id;
        weaponType = other.weaponType;
        prefab = other.prefab;
    }
}
