using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveCharacterAttribute : MonoBehaviour
{
    public Button openButton;
    public Button closeButton;
    public GameObject charAttribute;
    

    void Start()
    {
        openButton.onClick.AddListener(OpenThis);
        closeButton.onClick.AddListener(CloseThis);
    }

    public void OpenThis()
    {
        charAttribute.SetActive(true);
    }
    public void CloseThis()
    {
        charAttribute.SetActive(false);
    }

}
