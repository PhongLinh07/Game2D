using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

public abstract class ASlotUI : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField] public Image background;
    [SerializeField] public Image icon;
    [SerializeField] public TextMeshProUGUI countText;
    [SerializeField] public Image border;
    [SerializeField] public Image highlight;
    [SerializeField] public Image tick; //try

   
    public int thisIndex;

    protected void Init()
    {
      countText?.gameObject.SetActive(false);
      border?.gameObject.SetActive(false);
      highlight?.gameObject.SetActive(false);
      tick.gameObject.SetActive(false);

    }

    public void Reset()//Temporary
    {
        gameObject.SetActive(false);
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

    public abstract void SetData<T>(T data);

    public virtual void OnPointerDown(PointerEventData eventData)
    {
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
    }
}

