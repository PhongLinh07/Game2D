using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateSkillButton : ISkillButton, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    [SerializeField] private const float distanceWithOwner = 20.0f;
    [SerializeField] private const float angleOffset = -180.0f;

    protected override void CastSkill(params object[] args)
    {
        if (data?.Logic == null) return;
        StartCoroutine(data.Logic.Cast((Vector2)logicCharacter.transCenter.position, args[0]));
    }

    private IEnumerator StartCooldown()
    {
        cooldownOverlay.gameObject.SetActive(true);
        isOnCooldown = true;

        float timer = data.attrDict[EAttribute.Cd];
        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            cooldownText.text = Mathf.CeilToInt(timer).ToString();
            yield return null;
        }

        cooldownOverlay.gameObject.SetActive(false);
        isOnCooldown = false;
    }

    // ============================
    // Drag handlers
    // ============================
    public void OnPointerDown(PointerEventData eventData)
    {
        if (inputType != SkillInputType.Rotate || isOnCooldown) return;

        isDragging = true;
        dragWorld.gameObject.SetActive(true);
        ShowDragUI(eventData);

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        ShowDragUI(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;
        isDragging = false;
        dragWorld.gameObject.SetActive(false);

        if (!logicCharacter.CantUseSkill(data.id)) return;

        CastSkill(eventData.position -  RectTransformUtility.WorldToScreenPoint(null, rectTransform.position));
        StartCoroutine(StartCooldown());
    }

    private void ShowDragUI(PointerEventData eventData)
    {
        Vector2 buttonScreenPos = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);
        Vector2 dir = (eventData.position - RectTransformUtility.WorldToScreenPoint(null, rectTransform.position)).normalized;
        dragWorld.transform.position = (Vector2)Camera.main.WorldToScreenPoint(logicCharacter.transCenter.position) + dir * distanceWithOwner;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dragWorld.rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle - angleOffset);
    }
}
