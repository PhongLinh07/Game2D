using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityHFSM;

public class LogicCharacter : LogicUnit
{
    public CharacterUnit mOwner;
    private StateMachine<EAnimParametor, EFsmAction> fsm;
    private Rigidbody2D rb;
    private AnimatorController animatorController;
    private PlayerInputHandler playerInputHandler;

    public Transform bottomTrans;
    public Transform centerTrans;
   
    public Action OnStatsChanged; // only ui stats

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mOwner = GetComponent<CharacterUnit>(); mOwner.mlogic = this;
        animatorController = GetComponent<AnimatorController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        
        InitData();
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
                rb.velocity = playerInputHandler.moveInput * mOwner.combat.agility;
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

    public void EquipSkill(int idSkill)
    {
        mOwner.EquipSkill(idSkill);
    }

    public void RemoveEquipSkill(int idSkill)
    {
        mOwner.UnequipSkill(idSkill);
    }

    
    public override void UpdateUI()
    {
        OnStatsChanged?.Invoke();
    }

   
}
