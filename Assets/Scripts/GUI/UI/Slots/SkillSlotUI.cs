using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Slot or Cell
/// This is elementUI of Inventory
/// </summary>
public class SkillSlotUI : ASlotUI, IPointerClickHandler
{
    public Image equipped;
    public SkillCfgSkill dataOfSlot;

    private BattleSkillManager battleSkillManager;      // ScriptableObject chứa info skill

    private float doubleTapTime = 0.25f; // Khoảng thời gian tối đa giữa 2 lần tap
    private float lastTapTime = 0;

    private void Awake()
    {
        battleSkillManager = BattleSkillManager.Instance;      // ScriptableObject chứa info skill
    }

    public void Equip(bool state)
    {
        equipped.gameObject.SetActive(state);
    }

    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            Reset();
            return;
        }
        dataOfSlot = data as SkillCfgSkill;
        view.SetActive(true);
        icon.sprite = dataOfSlot.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Gọi container click bình thường
        AContainer<SkillCfgSkill> container = transform.parent.GetComponent<AContainer<SkillCfgSkill>>();
        container.OnClick(thisIndex);

        // ---------------- Double Tap logic ----------------
        if (Time.time < lastTapTime + doubleTapTime) TryEquipSkill();
        
        lastTapTime = Time.time;
    }

    private void TryEquipSkill()
    {
        if (battleSkillManager.EquipSKill(dataOfSlot))
            equipped.gameObject.SetActive(true);
        else
            equipped.gameObject.SetActive(false);
    }
}
