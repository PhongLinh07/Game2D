using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

/*
 dung cho tui tru vat, sach skil
*/

public abstract class AContainer<T>: MonoBehaviour
{
    private bool isInitialized = false; // chưa khởi tạo
    private int selectedSlot = 0;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject slotPrefab;
    public PlayerData playerData;

    protected List<ASlotUI> slotUIs = new();
    protected List<T> datas = new();

    private void OnEnable()
    {
        if(!isInitialized)Init();
        UpdateContainer();
    }
    public virtual void Init()
    {

        for (int i = 0; i < datas.Count; i++)
        {    
            ASlotUI newSlot = Instantiate(slotPrefab, parent).GetComponent<ASlotUI>();
            newSlot.SetIndex(i);
            slotUIs.Add(newSlot);
        }

        isInitialized = true;
    }

    public virtual void UpdateContainer() 
    {
        // Cập nhật vào UI
        for (int i = 0; i < datas.Count; i++)
        {
            slotUIs[i].SetData<T>(datas[i]);
        }
    }

    public virtual void OnClick(int index) 
    {
        HighlightSelectedSlot(index);
    }

    public virtual void HighlightSelectedSlot(int index)
    {
        slotUIs[selectedSlot].Highlight(false);
        selectedSlot = index;
        slotUIs[selectedSlot].Highlight(true);
    }
}
