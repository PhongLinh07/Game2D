using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateSkillButton : ISkillButton
{

    public RotateSkillButton(VisualTreeAsset template) : base(template) 
    {
        inputType = SkillInputType.Rotate;
    }

    // ============================
    // Drag handlers
    // ============================
    protected override void OnPointerDown(PointerDownEvent evt)
    {
        if (inputType != SkillInputType.Rotate || isOnCooldown) return;

        isDragging = true;
        Root.CaptureMouse(); // 🔒 Giữ chuột dù ra ngoài vùng
        IndicatorManager.Instance.Show(SkillInputType.Rotate, indicatorData.OriginPosition, indicatorData.Rotation);
    }

    protected override void OnPointerMove(PointerMoveEvent evt)
    {
        if (!isDragging) return;
        IndicatorManager.Instance.UpdateIndicator(SkillInputType.Rotate, indicatorData.OriginPosition, indicatorData.Rotation);
    }

    protected override void OnPointerUp(PointerUpEvent evt)
    {
        if (!isDragging) return;
        isDragging = false;
        Root.ReleaseMouse(); // 🔓 Trả chuột lại

        IndicatorManager.Instance.Hide(SkillInputType.Rotate);

        CastSkill();

    }


}
