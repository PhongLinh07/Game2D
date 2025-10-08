using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DragSkillButton : SkillButtonBase
{

    public DragSkillButton(VisualTreeAsset template) : base(template) { }

    protected override void OnPointerDown(PointerDownEvent evt)
    {
        Root.CaptureMouse(); // 👈 Giữ chuột

        if (inputType != SkillInputType.Drag || isOnCooldown) return;

        isDragging = true;
        IndicatorManager.Instance.Show(SkillInputType.Drag, indicatorData.Position, Quaternion.identity);
    }

    protected override void OnPointerMove(PointerMoveEvent evt)
    {

        if (!isDragging) return;
        IndicatorManager.Instance.UpdateIndicator(SkillInputType.Drag, indicatorData.Position, Quaternion.identity);
    }

    protected override void OnPointerUp(PointerUpEvent evt)
    {
        Root.ReleaseMouse(); // 👈 Trả chuột về bình thường

        if (!isDragging) return;
        isDragging = false;
        IndicatorManager.Instance.Hide(SkillInputType.Drag);

        CastSkill();
  
    }


}
