using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UICard : MonoBehaviour
{
    public TextMeshProUGUI titleUI;
    public string title;

    public TextMeshProUGUI contentUI;
    public string content;

    public void SetContent(string content)
    {
        if (!gameObject.activeSelf) return;
        contentUI.text = content;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (titleUI != null) titleUI.text = title;
        if (contentUI != null) contentUI.text = content;
    }
#endif
}
