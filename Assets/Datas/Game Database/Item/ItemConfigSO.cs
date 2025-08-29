using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/Item")]
public class ItemConfigSO : ScriptableObject
{
    public List<ItemCfgItem> datas;
    public void LoadData()
    {
        ItemConfig.GetInstance.InitData(datas);
    }
}
