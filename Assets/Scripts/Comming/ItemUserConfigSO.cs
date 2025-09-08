using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "DataBase/ItemUser")]
public class ItemUserConfigSO : ScriptableObject
{
    public List<ItemUserCfgItem> datas;
    private void OnValidate()
    {
        for (int i = 0; i < datas.Count; i++)
        {
            datas[i].id = i;
        }
    }
}
