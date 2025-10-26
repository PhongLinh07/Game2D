using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragSkillButton : ASkillButton, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (inputType != SkillInputType.Drag || isOnCooldown) return;

        isDragging = true;
        UpdateLogic(eventData.position);
        IndicatorManager.Instance.Show(SkillInputType.Drag, indicatorData.Position, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (!isDragging) return;
        UpdateLogic(eventData.position);
        IndicatorManager.Instance.UpdateIndicator(SkillInputType.Drag, indicatorData.Position, Quaternion.identity);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        if (!isDragging) return;
        isDragging = false;
        UpdateLogic(eventData.position);
        IndicatorManager.Instance.Hide(SkillInputType.Drag);

        CastSkill();

    }


}
