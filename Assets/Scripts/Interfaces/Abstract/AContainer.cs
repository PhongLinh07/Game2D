using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

/*
 dung cho tui tru vat, sach skil
*/

public abstract class AContainer<T> : MonoBehaviour where T : ConfigItem
{
    [SerializeField] protected UIDocument uiDocument;
    [SerializeField] protected VisualTreeAsset slotTemplate;
    protected bool isInitialized = false; // chưa khởi tạo
    protected int selectedSlot = 0;

    

    //protected List<ASlotUI> slotUIs = new();
    protected Dictionary<int, ASlotUI> slotUIs = new();

    private void OnEnable()
    {
      //  if(!isInitialized)Init();
        UpdateContainer();
    }
    public virtual void Init()
    {

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
