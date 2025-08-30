using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveCharacterAttribute : MonoBehaviour
{
    public static ActiveCharacterAttribute Instance;
    public Button closeButton;
    public GameObject panel;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        closeButton.onClick.AddListener(CloseThis);
        HUDController.Instance.avatar.onClick.AddListener(OpenThis);
    }

    public void OpenThis()
    {
        panel.SetActive(true);
    }
    public void CloseThis()
    {
        panel.SetActive(false);
    }

}
