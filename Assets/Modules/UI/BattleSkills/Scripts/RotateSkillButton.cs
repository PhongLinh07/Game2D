using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RotateSkillButton : ASkillButton, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private void Start()
    {
        inputType = SkillInputType.Rotate;
    }

    // ============================
    // Drag handlers
    // ============================
    public void OnPointerDown(PointerEventData eventData)
    {
        if (inputType != SkillInputType.Rotate || isOnCooldown) return;
        isDragging = true;
        UpdateLogic(eventData.position);
        IndicatorManager.Instance.Show(SkillInputType.Rotate, indicatorData.OriginPosition, indicatorData.Rotation);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        UpdateLogic(eventData.position);
        IndicatorManager.Instance.UpdateIndicator(SkillInputType.Rotate, indicatorData.OriginPosition, indicatorData.Rotation);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;
        isDragging = false;
        UpdateLogic(eventData.position);
        IndicatorManager.Instance.Hide(SkillInputType.Rotate);
        CastSkill();

    }


}
