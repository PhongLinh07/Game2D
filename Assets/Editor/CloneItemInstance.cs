using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemUserConfigSO))]
public class CloneItemInstance : Editor
{
    public override void OnInspectorGUI()
    {      
        ItemUserConfigSO db = (ItemUserConfigSO)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Clone Instance Item"))
        {
           

            // Xóa list
            for(int i = 0; i <= UUIDConfig.GetInstance.GetUUID(EUUIDType.ItemTemplate); i++)
            {
                ItemUserCfgItem itemClone = Tool_Clone_Item_Instance.GetInstance.Clone();

                ItemUserCfgItem item = db.datas.Find(i => i.id_Item == itemClone.id_Item);

                if (item == null)
                {
                    itemClone.id = UUIDConfig.GetInstance.GeneratorUUID(EUUIDType.ItemInstance);
                    db.datas.Add(itemClone);
                    continue;
                }

                if (item != null && ItemConfig.GetInstance.GetConfigItem(item.id_Item).Stackable)
                {
                    item.Quantity += itemClone.Quantity;
                }
                else
                {
                    itemClone.id = UUIDConfig.GetInstance.GeneratorUUID(EUUIDType.ItemInstance);
                    db.datas.Add(itemClone);
                }


            }

            // Đánh dấu asset đã thay đổi để lưu
            EditorUtility.SetDirty(db);
            ItemUserConfig.GetInstance.mDatas = db.datas;
        }

        if (GUILayout.Button("Clear All"))
        { 
            db.datas.Clear();
            EditorUtility.SetDirty(db);
            UUIDConfig.GetInstance.mCfgDict[EUUIDType.ItemInstance].uuidCurrent = -1;
            ItemUserConfig.GetInstance.mDatas = db.datas;
            // Đánh dấu asset đã thay đổi để lưu
        }

        // Vẽ inspector mặc định
        DrawDefaultInspector();

    }

}
