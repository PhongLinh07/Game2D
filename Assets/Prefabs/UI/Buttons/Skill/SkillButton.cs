using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ISkillButton : MonoBehaviour
{
    [Header("UI Components")]
    public Image skillIcon;          // Icon skill hiển thị
    public Image cooldownOverlay;    // Overlay hiển thị cooldown
    public TextMeshProUGUI cooldownText;
    public Button btnSkill;          // Button UI

    [Header("Skill Data")]
    public SkillCfgSkill data;      // ScriptableObject chứa info skill
    public SkillInputType inputType = SkillInputType.Drag; // Kiểu input skill
   
    protected bool isOnCooldown = false;
    protected Transform transOfPlayer;
    public void SetData(Transform player, SkillCfgSkill skill)
    {
        transOfPlayer = player;
        data = skill;

        if (btnSkill != null) btnSkill.onClick.AddListener(OnTapSkill);

        if (skillIcon != null && data != null) skillIcon.sprite = data.Icon;

        if (cooldownOverlay != null) cooldownOverlay.gameObject.SetActive(false); // ban đầu không phủ

        if (cooldownText != null) cooldownText.text = "0"; // ban đầu không phủ

        cooldownOverlay.gameObject.SetActive(false);

        ASkillLogic.GetLogic(data);
    }

    public abstract void OnTapSkill();
}

public class SkillButton : ISkillButton
{
    
    // Khi nhấn nút
    public override void OnTapSkill()
    {
        if (isOnCooldown || data == null) return;

        // Kiểm tra kiểu skill
        if (inputType == SkillInputType.Tap)
        {
            CastSkill();
            StartCoroutine(StartCooldown());
        }
    }

    private void CastSkill()
    {
        if (data.Logic == null) return;
        StartCoroutine(data.Logic.Cast((Vector2)(transOfPlayer.position)));
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
}
