using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControllerCenter : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject toolBarpanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActiveControllerCenter();
        }
    }

    public void ActiveControllerCenter()
    {
        //activeInHierarchy   👉 Có đang hoạt động thực sự trong scene, tính cả cha không?
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        toolBarpanel.SetActive(!toolBarpanel.activeInHierarchy);
    }
}
