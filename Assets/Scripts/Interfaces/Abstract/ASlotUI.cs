using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
public abstract class ASlotUI : MonoBehaviour
{
    public GameObject view;
    public Image icon;
    public Image highlight;
    public int thisIndex;

    public abstract void SetData<T>(T data);

    public void Reset()//Temporary
    {
        view.SetActive(false);
        highlight.gameObject.SetActive(false);
    }

    public void SetIndex(int index)
    {
        thisIndex = index;
    }

    public void Highlight(bool state)
    {
        highlight.gameObject.SetActive(state);
    }   
}

