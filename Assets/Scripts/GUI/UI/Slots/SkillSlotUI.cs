using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

/// <summary>
/// Slot or Cell
/// This is elementUI of Inventory
/// </summary>
public class SkillSlotUI : ASlotUI
{
    
    public SkillCfgItem dataOfSlot;

    private BattleSkillManager battleSkillManager;      // ScriptableObject chứa info skill

    private float doubleTapTime = 0.25f; // Khoảng thời gian tối đa giữa 2 lần tap
    private float lastTapTime = 0;

    public SkillSlotUI(VisualTreeAsset template) : base(template)
    {
        battleSkillManager = BattleSkillManager.Instance;      // ScriptableObject chứa info skill
    }



    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            Reset();
            return;
        }
        dataOfSlot = data as SkillCfgItem;
        Icon.style.backgroundImage = new StyleBackground(dataOfSlot.Icon);

    }

    protected override void OnClick(ClickEvent evt)
    {
        // Gọi container click bình thường
        SkillBook.Instance.OnClick(dataOfSlot.id);

        // ---------------- Double Tap logic ----------------
        if (Time.time < lastTapTime + doubleTapTime) TryEquipSkill();
        
        lastTapTime = Time.time;
    }

    private void TryEquipSkill()
    {
        battleSkillManager.EquipSKill(dataOfSlot);
        SkillBook.Instance.UpdateContainer();
    }
}
