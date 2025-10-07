using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// Slot or Cell
/// This is elementUI of Inventory
/*
 *  Slot( Button, Script_SlotUI)
 *    |___View
 *    |     |___Icon( Image )
 *    |
 *    |___Highlight( Image )
*/
/// </summary>

public abstract class ASlotUI
{
    public VisualElement Root { get; private set; }
    protected VisualElement Background { get; private set; }
    protected VisualElement _Highlight { get; private set; }
    protected VisualElement Icon { get; private set; }
    protected VisualElement Tick { get; private set; }

  

    protected ASlotUI(VisualTreeAsset template)
    {
        Root = template.Instantiate();

        // Trong ItemSlotUI
        Root.style.width = 128;
        Root.style.height = 128;
        Root.style.marginRight = 4;
        Root.style.marginBottom = 4;

        Background = Root.Q<VisualElement>("Background");
        Icon = Root.Q<VisualElement>("Icon");
        _Highlight = Root.Q<VisualElement>("Highlight");
        Tick = Root.Q<VisualElement>("Tick");

        _Highlight.style.display = DisplayStyle.None;
        Tick.style.display = DisplayStyle.None;
        // Đăng ký event click
        Root.RegisterCallback<ClickEvent>(OnClick);

    }

    public void Equip(bool state)
    {
        Tick.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void Reset()//Temporary
    {
      
       // highlight.gameObject.SetActive(false);
    }

   
    protected virtual void OnClick(ClickEvent evt)
    {
       // Debug.Log("Slot Clicked");
    }

    public void Highlight(bool state)
    {
        _Highlight.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public abstract void SetData<T>(T data);

}

