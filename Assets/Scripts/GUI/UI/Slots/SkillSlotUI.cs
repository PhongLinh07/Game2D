using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Slot or Cell
/// This is elementUI of Inventory
/// </summary>
public class SkillSlotUI : ASlotUI, IPointerClickHandler
{
    
    public SkillCfgItem dataOfSlot;

    private BattleSkillManager battleSkillManager;      // ScriptableObject chứa info skill

    private float doubleTapTime = 0.25f; // Khoảng thời gian tối đa giữa 2 lần tap
    private float lastTapTime = 0;

    private void Awake()
    {
        battleSkillManager = BattleSkillManager.Instance;      // ScriptableObject chứa info skill
        base.Init();
    }


    public void Equip(bool state)
    {
        tick.gameObject.SetActive(state);
    }

    public override void SetData<T>(T data)
    {
        if (data == null)
        {
            Reset();
            return;
        }
        dataOfSlot = data as SkillCfgItem;
        gameObject.SetActive(true);
        icon.sprite = dataOfSlot.Icon;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        // Gọi container click bình thường
        AContainer<SkillCfgItem> container = transform.parent.GetComponent<AContainer<SkillCfgItem>>();
        container.OnClick(thisIndex);

        // ---------------- Double Tap logic ----------------
        if (Time.time < lastTapTime + doubleTapTime) TryEquipSkill();
        
        lastTapTime = Time.time;
    }

    private void TryEquipSkill()
    {
        if (battleSkillManager.EquipSKill(dataOfSlot))
            tick.gameObject.SetActive(true);
        else
            tick.gameObject.SetActive(false);
    }
}
