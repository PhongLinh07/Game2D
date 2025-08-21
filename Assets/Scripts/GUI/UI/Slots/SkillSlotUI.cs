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
 *    |
 *    |___View
 *    |     |___Icon( Image )
 *    |
 *    |___Highlight( Image )
*/
/// </summary>

public class SkillSlotUI : ASlotUI, IPointerClickHandler
{
    public Image equipped;
    public SkillCfgSkill dataOfSlot;
    
    private BattleSkillManager battleSkillManager;      // ScriptableObject chứa info skill

    private void Awake()
    {
        battleSkillManager = BattleSkillManager.Instance;      // ScriptableObject chứa info skill
    }

    public void Equip(bool state)
    {
        equipped.gameObject.SetActive(state);
    }

    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            Reset();
            return;
        }
        dataOfSlot = data as SkillCfgSkill;
        view.SetActive(true);
        icon.sprite = dataOfSlot.Icon;
    }
     
    public void OnPointerClick(PointerEventData eventData)
    {
        AContainer<SkillCfgSkill> container = transform.parent.GetComponent<AContainer<SkillCfgSkill>>();
        // Gọi hàm Instance đã override từ hàm ảo ItemPanel
        container.OnClick(thisIndex);

        if (eventData.clickCount != 2) return;


        if (battleSkillManager.EquipSKill(dataOfSlot))
        {
            equipped.gameObject.SetActive(true);
        }
        else
        {
            equipped.gameObject.SetActive(false);
        }

    }

}