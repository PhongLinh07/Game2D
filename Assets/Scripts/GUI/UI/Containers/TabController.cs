using Microsoft.Unity.VisualStudio.Editor;
using System;
using UnityEngine;

public class TabController : MonoBehaviour
{
    [Header("Highlight of Button")]
    public GameObject[] tabHighlight; // Try
    [Header("Panel of Button")]
    public GameObject[] tabPanels;


    private void Start()
    {
        for (int i = 0; i < tabPanels.Length; i++)
        {
            tabPanels[i].SetActive(false); // chỉ hiện tab được chọn
            tabHighlight[i].SetActive(false); // chỉ hiện tab được chọn
        }

        tabPanels[0]?.SetActive(true); // chỉ hiện tab được chọn
        tabHighlight[0]?.SetActive(true); // chỉ hiện tab được chọn
    }
    // Hàm để mở tab theo chỉ số (index)
    public void OpenTab(int index)
    {
        for (int i = 0; i < tabPanels.Length; i++)
        {
            tabPanels[i].SetActive(i == index); // chỉ hiện tab được chọn
            tabHighlight[i].SetActive(i == index); // chỉ hiện tab được chọn
        }
    }

}
