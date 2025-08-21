using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSkillButton : ISkillButton, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
   
    // Drag variables
    private bool isDragging = false;
    private Vector2 dragPosition;
    public Image dragWorld;

    private Vector3 velocity = Vector3.zero; // đặt ở ngoài hàm Update để giữ trạng thái
    // Khi nhấn nút (Tap)
    public override void OnTapSkill()
    {
        if (isOnCooldown || data == null) return;
    }

    // Cast skill với vị trí target
    private void CastSkill(Vector2 targetPos)
    {
        if (data.Logic == null) return;
        StartCoroutine(data.Logic.Cast(targetPos));
    }

    // Hiển thị cooldown
    private IEnumerator StartCooldown()
    {
        cooldownOverlay.gameObject.SetActive(true);
        isOnCooldown = true;

        float timer = data.cooldown;

        while (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            cooldownText.text = Mathf.CeilToInt(timer).ToString();
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
        dragWorld.gameObject.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        dragPosition = eventData.position;

        Vector2 buttonScreenPos = RectTransformUtility.WorldToScreenPoint(null, btnSkill.transform.position);
        Vector2 drag = eventData.position - buttonScreenPos; // Pixel
        float distance = drag.magnitude;

        distance = Mathf.Clamp01(distance / 32.0f) * 5.0f;
        
        Vector2 taget = (Vector2)transOfPlayer.position + drag.normalized * distance;

        dragWorld.transform.position = Vector3.SmoothDamp(dragWorld.transform.position, Camera.main.WorldToScreenPoint(taget), ref velocity, 0.05f); // Gán trực tiếp vì Overlay dùng screen pixel

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;
        isDragging = false;
        dragWorld.gameObject.SetActive(false);

        Vector2 buttonScreenPos = RectTransformUtility.WorldToScreenPoint(null, btnSkill.transform.position);
        Vector2 drag = eventData.position - buttonScreenPos; // Pixel
        float distance = drag.magnitude;
        
        distance = Mathf.Clamp01(distance / 32.0f) * 5.0f;
        Debug.Log(distance);

        Vector2 taget = (Vector2)transOfPlayer.position + drag.normalized * distance;
        // Khi thả, cast skill tại vị trí kéo
        CastSkill(taget);
        StartCoroutine(StartCooldown());
    }

}
