using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public abstract class ISkillButton : MonoBehaviour
{
    [Header("UI References")]
    public Image skillIcon;              // Icon skill chính (UI Image gốc)
    public Image cooldownOverlay;        // Hình cover cooldown (UI Image)
    public TextMeshProUGUI cooldownText; // Số giây còn lại cooldown
    public Image dragWorld;              // Icon target di chuyển theo drag


    [Header("Skill Data")]
    public SkillCfgItem data;               // ScriptableObject / data chứa logic, cooldown
    protected LogicCharacter logicCharacter;       // Transform nhân vật
    public SkillInputType inputType = SkillInputType.Drag;


    protected bool isDragging = false;
    protected bool isOnCooldown = false;
    protected Vector3 velocity = Vector3.zero;
    protected RectTransform rectTransform;
  
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (cooldownText != null) cooldownText.text = "0"; // ban đầu không phủ
        if (cooldownOverlay != null) cooldownOverlay.gameObject.SetActive(false);
        if (dragWorld != null) dragWorld.gameObject.SetActive(false);
    }

    public void SetData(LogicCharacter logic, SkillCfgItem skill)
    {
        logicCharacter = logic;
        data = skill;

        if (skillIcon != null && data != null) skillIcon.sprite = data.Icon;

        ASkillLogic.GetLogic(data);
    }
    protected abstract void CastSkill(params object[] args);
}
 class SkillButton : ISkillButton, IPointerUpHandler
{
    
    protected override void CastSkill(params object[] args)
    {
        if (data.Logic == null) return;
        StartCoroutine(data.Logic.Cast((Vector2)(logicCharacter.bottomTrans.position)));
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

    
    public void OnPointerUp(PointerEventData eventData)
    {
        CastSkill();
        StartCoroutine(StartCooldown());
    }

}
