using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityHFSM;
using static UnityEditor.Progress;

public class LogicCharacter : LogicUnit
{
    public static LogicCharacter Instance;

    [SerializeField] private CharacterUnit mOwner;
    private StateMachine<EAnimParametor, EFsmAction> fsm;
    private Rigidbody2D rb;
    private AnimatorController animatorController;
    private PlayerInputHandler playerInputHandler;

    public Transform transBottom;
    public Transform transCenter;

    public Action OnStatsChanged; // only ui stats
    public Action<EWeaponType> OnWeaponChanged; // only ui stats
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mOwner = GetComponent<CharacterUnit>();
        animatorController = GetComponent<AnimatorController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        if (!mOwner) Debug.Log("null");
        InitData();

        OnWeaponChanged += mOwner.IsWeaponValid;
    }
    public override void InitData()
    {
        fsm = new StateMachine<EAnimParametor, EFsmAction>();

        // IDLE
        fsm.AddState(EAnimParametor.Idle,
            onEnter: ctx => { animatorController.SetState(EAnimParametor.Idle); },
            onLogic: ctx =>
            {
                if (playerInputHandler.moveInput.magnitude > 0.01f) fsm?.RequestStateChange(EAnimParametor.Run);
            }
        );

        // MOVE
        fsm.AddState(EAnimParametor.Run,
            onEnter: ctx => { animatorController.SetState(EAnimParametor.Run); },
            onLogic: ctx =>
            {
                rb.velocity = playerInputHandler.moveInput * mOwner.GetAttributes(EAttribute.Speed).value;
                if (playerInputHandler.moveInput.magnitude < 0.01f) fsm?.RequestStateChange(EAnimParametor.Idle);
            },
            onExit: ctx => rb.velocity = Vector2.zero
        );

        // TRANSITIONS
        fsm.SetStartState(EAnimParametor.Idle);
        fsm.Init();

    }


    void Update()
    {
        fsm.OnLogic();

        mOwner.SetPosition(transform.position);
    }

    public CharacterUnit Data
    {
        get { return mOwner; }
    }

    public bool Equipment(EEquipmentType equipType, ItemUserCfgItem item)
    {
        bool cantEquip = false;
        cantEquip = mOwner.Equipment(equipType, item);
        OnWeaponChanged(mOwner.GetWeaponCurrent());

        return cantEquip;
    }
    public void Unequipment(EEquipmentType equipType)
    {
        mOwner.Unequipment(equipType);
        OnWeaponChanged(mOwner.GetWeaponCurrent());
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public override void Teleport(Vector2 position)
    {
        transform.position = position;
    }

    public override void TakeDamage(int damage)
    {
        mOwner.TakeDamage(damage);
        UpdateUI();
    }

    public void Heal(int amount)
    {
        mOwner.Heal(amount);
        UpdateUI();
    }

    public bool EquipSkill(SkillCfgItem skill)
    {
        return mOwner.EquipSkill(skill);
    }

    public void UnequipSkill(SkillCfgItem skill)
    {
        mOwner.UnequipSkill(skill);
    }

    public bool CantUseSkill(int idSkill)
    {
       // float qiCons = SkillConfig.GetInstance.GetConfigItem(idSkill).attrDict[EAttribute.EnergyCost];

       // if(mOwner.Data.general.currEnergy < qiCons) return false;
        return true;
    }

    public void UseSkill(int idSkill)
    {
       // mOwner.UseSkill(idSkill);
      //  UpdateUI();
    }

    public override void UpdateUI()
    {
        OnStatsChanged?.Invoke();
    }

   
}
