using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/ItemUser")]
public class ItemUserConfigSO : ScriptableObject
{
    public List<ItemUserCfgItem> datas;
}
