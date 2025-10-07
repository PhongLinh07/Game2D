using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


public struct IndicatorData
{
    public Vector2 OriginPosition; // Vị trí vũ khí
    public Vector2 Direction; // Hướng tấn công
    public Vector2 Position; // vị trí trận pháp
    public Quaternion Rotation; // góc xoay ttuowng đối 
}




public abstract class ISkillButton : MonoBehaviour
{
   
    [Header("UI Elements")]
    public VisualElement Root;
    private VisualElement Icon;
    private VisualElement Overlay;
    private Label Text;


    [Header("Datas ")]
    public SkillCfgItem data;               // ScriptableObject / data chứa logic, cooldown
    protected LogicCharacter logicCharacter;       // Transform nhân vật
    protected SkillInputType inputType = SkillInputType.Drag;

    [Header("Internal Datas")]
    protected bool isDragging = false;
    protected bool isOnCooldown = false;
    protected IndicatorData indicatorData;


    void Awake()
    {
        
    }

    protected ISkillButton(VisualTreeAsset template)
    {
        Root = template.Instantiate();

        // Trong ItemSlotUI
        Root.style.width = 128;
        Root.style.height = 128;
        Root.style.marginRight = 16;
        Root.style.marginLeft = 16;

        Icon = Root.Q<VisualElement>("Icon");
        Overlay = Root.Q<VisualElement>("Overlay");
        Text = Root.Q<Label>("Text");

        Overlay.style.display = DisplayStyle.None;
        Text.style.display = DisplayStyle.None;

        Root.RegisterCallback<PointerDownEvent>(evt => { UpdateLogic(evt.position); OnPointerDown(evt);});
        Root.RegisterCallback<PointerMoveEvent>(evt => { UpdateLogic(evt.position); OnPointerMove(evt);});
        Root.RegisterCallback<PointerUpEvent>(evt => { UpdateLogic(evt.position); OnPointerUp(evt);});
    }
    public void SetData(LogicCharacter logic, SkillCfgItem skill)
    {
        logicCharacter = logic;
        data = skill;

        if (Icon != null && data != null) Icon.style.backgroundImage = new StyleBackground(data.Icon);

        ASkillLogic.GetLogic(data);
    }

    protected abstract void OnPointerDown(PointerDownEvent evt);
    protected abstract void OnPointerMove(PointerMoveEvent evt);
    protected abstract void OnPointerUp(PointerUpEvent evt);


    protected void CastSkill()
    {
        if (data?.Logic == null || isOnCooldown) return;
        logicCharacter.StartCoroutine(data.Logic.Cast(indicatorData));
        logicCharacter.StartCoroutine(StartCooldown());

    }


    private IEnumerator StartCooldown()
    {
        Overlay.style.display = DisplayStyle.Flex;
        isOnCooldown = true;

        float timer = data.attrDict[EAttribute.Cooldown];
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            Text.text = Mathf.CeilToInt(timer).ToString();
            yield return null;
        }

        Overlay.style.display = DisplayStyle.None;
        isOnCooldown = false;
    }


    // dùng tính vị trí tương đối với nhân vật (Drag)
    protected void UpdateLogic(Vector2 screenPos)
    {
        // 1️⃣ Đảo trục Y vì UI Toolkit có gốc top-left, còn Screen gốc bottom-left
        screenPos.y = Screen.height - screenPos.y;

        // 2️⃣ Lấy tâm của nút skill (cũng cần đảo trục y tương tự)
        Vector2 center = Root.worldBound.center;
        center.y = Screen.height - center.y;

        // 3️⃣ Tính vector kéo
        indicatorData.Direction = screenPos - center;

        // 4️⃣ Chuẩn hóa độ dài thành khoảng cách trong world
        float distance = Mathf.Clamp01(indicatorData.Direction.magnitude / 64f) * 5f; // try | * 5

        // 5️⃣ Tính vị trí mục tiêu trong world
        indicatorData.Position = (Vector2)logicCharacter.GetPosition() + indicatorData.Direction.normalized * distance;

        // 6️⃣ Tính góc xoay (theo UI)
        float angle = Mathf.Atan2(indicatorData.Direction.y, indicatorData.Direction.x) * Mathf.Rad2Deg;

        // 7️⃣ Tạo quaternion để quay indicator
        indicatorData.Rotation = Quaternion.Euler(0f, 0f, angle);

        indicatorData.OriginPosition = logicCharacter.GetCastSkilPosition();
    }

}
