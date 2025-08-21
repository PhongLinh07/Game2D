using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateSkillButton : ISkillButton, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private Vector2 dragPosition;
    public Image dragWorld;

    private Vector3 velocity = Vector3.zero;

    // Nhấn nhanh (tap)
    public override void OnTapSkill()
    {
        if (isOnCooldown || data == null) return;

        //// Cast ngay hướng mặc định (hướng nhìn Player)
        //Vector2 dir = Vector2.right; // Hoặc lấy từ Player.currentFacing
        //CastSkill(dir);
        //StartCoroutine(StartCooldown());
    }

    private void CastSkill(Vector2 direct)
    {
        if (data?.Logic == null) return;
        StartCoroutine(data.Logic.Cast((Vector2)transOfPlayer.position, direct));
    }

    private IEnumerator StartCooldown()
    {
        cooldownOverlay.gameObject.SetActive(true);
        isOnCooldown = true;

        float timer = data.cooldown;
        while (timer > 0f)
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

        dragWorld.transform.position = Vector3.SmoothDamp(dragWorld.transform.position, Camera.main.WorldToScreenPoint(taget), ref velocity, 0.05f); // Gán trực t
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;
        isDragging = false;
        dragWorld.gameObject.SetActive(false);

        Vector2 buttonScreenPos = RectTransformUtility.WorldToScreenPoint(null, btnSkill.transform.position);
        Vector2 drag = eventData.position - buttonScreenPos;

        float distance = Mathf.Clamp01(drag.magnitude / 32f) * 5f;
        Vector2 target = (Vector2)transOfPlayer.position + drag.normalized * distance;

        // Cast theo hướng kéo
        Vector2 direction = (target - (Vector2)transOfPlayer.position).normalized;
        CastSkill(direction);
        StartCoroutine(StartCooldown());
    }
}
