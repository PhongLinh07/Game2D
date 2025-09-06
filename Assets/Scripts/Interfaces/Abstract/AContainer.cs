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

public abstract class AContainer<T> : MonoBehaviour where T : ConfigItem
{
    protected bool isInitialized = false; // chưa khởi tạo
    private int selectedSlot = 0;
    [SerializeField] protected Transform parent;
    [SerializeField] protected GameObject slotPrefab;
    

    //protected List<ASlotUI> slotUIs = new();
    protected Dictionary<int, ASlotUI> slotUIs = new();
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
            newSlot.SetData<T>(datas[i]);
            slotUIs.Add(datas[i].id, newSlot);
            
        }

        isInitialized = true;
    }

    public virtual void UpdateContainer() { }

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
