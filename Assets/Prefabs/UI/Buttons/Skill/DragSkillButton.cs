using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSkillButton : ISkillButton, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
   

    // Cast skill với vị trí target
    protected override void CastSkill(params object[] args)
    {
        if (data.Logic == null) return;
        StartCoroutine(data.Logic.Cast(args[0]));
    }

    // Hiển thị cooldown
    private IEnumerator StartCooldown()
    {
        cooldownOverlay.gameObject.SetActive(true);
        isOnCooldown = true;

        float timer = data.attrDict[EAttribute.Cd];

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (cooldownText != null) cooldownText.text = Mathf.CeilToInt(timer).ToString();
            yield return null;
        }

        cooldownOverlay.gameObject.SetActive(false);
        isOnCooldown = false;
    }

    // -----------------------
    // Drag handlers
    // -----------------------
    public void OnPointerDown(PointerEventData eventData)
    {
        if (inputType != SkillInputType.Drag || isOnCooldown) return;

        isDragging = true;
        if (dragWorld != null) dragWorld.gameObject.SetActive(true);
        ShowDragUI(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        ShowDragUI(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging ) return;
        isDragging = false;
        if (dragWorld != null) dragWorld.gameObject.SetActive(false);

        if (!logicCharacter.CantUseSkill(data.id)) return;

        Vector2 drag = eventData.position - RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
        float distance = drag.magnitude;

        distance = Mathf.Clamp01(distance / 64.0f) * 5.0f;

        Vector2 taget = (Vector2)logicCharacter.GetPosition() + drag.normalized * distance;

        // Khi thả, cast skill tại vị trí kéo
        CastSkill(taget);
        StartCoroutine(StartCooldown());
    }

    private void ShowDragUI(PointerEventData eventData)
    {
        Vector2 drag = eventData.position - RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
        float distance = drag.magnitude;

        distance = Mathf.Clamp01(distance / 64) * 5.0f;

        Vector2 taget = (Vector2)logicCharacter.GetPosition() + drag.normalized * distance;

        if (dragWorld == null) return;
        dragWorld.transform.position = Camera.main.WorldToScreenPoint(taget); // Gán trực tiếp vì Overlay dùng screen pixel
    }
}
