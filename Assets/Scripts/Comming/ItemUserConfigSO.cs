using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/ItemUser")]
public class ItemUserConfigSO : ScriptableObject
{
    public List<ItemUserCfgItem> datas;
    private void OnValidate()
    {
        foreach (var item in datas)
        {
            if(item != null) item.uuid = UUID.GetInstance.Generator();
        }
    }
}
