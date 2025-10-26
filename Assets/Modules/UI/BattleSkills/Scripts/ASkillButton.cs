using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public struct IndicatorData
{
    public Vector2 OriginPosition; // Vị trí vũ khí
    public Vector2 Direction; // Hướng tấn công
    public Vector2 Position; // vị trí trận pháp
    public Quaternion Rotation; // góc xoay ttuowng đối 
}


public abstract class ASkillButton : MonoBehaviour
{
    [Header("UI References")]
    public Image skillIcon;              // Icon skill chính (UI Image gốc)
    public Image cooldownOverlay;        // Hình cover cooldown (UI Image)
    public TextMeshProUGUI cooldownText; // Số giây còn lại cooldown


    [Header("Skill Data")]
    public SkillCfgItem data;               // ScriptableObject / data chứa logic, cooldown
    protected LogicCharacter logicCharacter;       // Transform nhân vật
    public SkillInputType inputType = SkillInputType.Drag;

    protected IndicatorData indicatorData;

    protected bool isDragging = false;
    protected bool isOnCooldown = false;
    protected Vector3 velocity = Vector3.zero;
    protected RectTransform rectTransform;
  
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (cooldownText != null) cooldownText.text = "0"; // ban đầu không phủ
        if (cooldownOverlay != null) cooldownOverlay.gameObject.SetActive(false);
    }

    public void SetData(LogicCharacter logic, SkillCfgItem skill)
    {
        logicCharacter = logic;
        data = skill;

        if (skillIcon && data != null) skillIcon.sprite = data.Icon;

        ASkillLogic.GetLogic(data);
    }

    protected void CastSkill()
    {
        if (data?.Logic == null || isOnCooldown) return;
        logicCharacter.StartCoroutine(data.Logic.Cast(indicatorData));
        logicCharacter.StartCoroutine(StartCooldown());

    }

    private IEnumerator StartCooldown()
    {
        cooldownOverlay.gameObject.SetActive(true);
        isOnCooldown = true;

        float timer = data.attrDict[EAttribute.Cooldown];

        while (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            cooldownText.text = Mathf.CeilToInt(timer).ToString();
            yield return null;
        }

        cooldownOverlay.gameObject.SetActive(false);
        isOnCooldown = false;
    }

    // dùng tính vị trí tương đối với nhân vật (Drag)
    protected void UpdateLogic(Vector2 screenPos)
    {
        if (logicCharacter == null) return;

        // 1️⃣ Lấy tâm nút skill trong màn hình (Screen space)
        Vector2 center = RectTransformUtility.WorldToScreenPoint(null, rectTransform.position);

        // 2️⃣ Tính vector kéo từ tâm nút đến vị trí chuột/touch
        Vector2 drag = screenPos - center;

        // 3️⃣ Chuẩn hóa độ dài thành khoảng cách trong world
        float distance = Mathf.Clamp01(drag.magnitude / 64f) * 5f;

        // 4️⃣ Tính vị trí mục tiêu trong world
        Vector2 targetWorldPos = (Vector2)logicCharacter.GetPosition() + drag.normalized * distance;
        indicatorData.Position = targetWorldPos;

        // 5️⃣ Tính vector hướng và góc xoay
        indicatorData.Direction = drag.normalized;
        float angle = Mathf.Atan2(drag.y, drag.x) * Mathf.Rad2Deg;
        indicatorData.Rotation = Quaternion.Euler(0f, 0f, angle);

        // 6️⃣ Lấy vị trí origin cast skill từ nhân vật
        indicatorData.OriginPosition = logicCharacter.GetCastSkilPosition();
    }


}

