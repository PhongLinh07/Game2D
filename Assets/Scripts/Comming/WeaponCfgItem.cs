using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum EWeaponType
{
    None = 0,
    Sword = 1,
    Thunder = 2,
    Ice = 3,
    Wind = 4,
    Fire = 5

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
