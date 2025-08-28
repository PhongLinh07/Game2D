using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityHFSM;

public class LogicMonster : LogicUnit
{
    public MonsterUnit mOwner;
    public StateMachine<EAnimParametor, EFsmAction> fsm;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private EnemyAnimatorController animatorController;
    private EnemyAIController enemyAIController;

    public Vector2 directionToTarget = Vector2.zero;
    private float distanceToTarget = 100.0f;


    public Transform transOfPlayer;
    public float chaseRadius;
    public float attackRadius;

    public StatusBar hp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mOwner = GetComponent<MonsterUnit>();
        animatorController = GetComponent<EnemyAnimatorController>();
        enemyAIController = GetComponent<EnemyAIController>();

        hp.Init(mOwner.general.vitality, mOwner.general.vitality);
        hp.gameObject.SetActive(false);

        InitData();
    }
    public override void InitData()
    {
        fsm = new StateMachine<EAnimParametor, EFsmAction>();


        fsm.AddState(EAnimParametor.None);

        // IDLE
        fsm.AddState(EAnimParametor.Idle,
            onEnter: ctx =>
            {
                animatorController.SetState(EAnimParametor.Idle);
                rb.velocity = Vector2.zero;
            },
            onLogic: ctx =>
            {
                if (isAttacking) return;

                if (distanceToTarget <= attackRadius)
                {
                    direction = directionToTarget;
                    enemyAIController?.HandleAttackRequest();
                    Pause();
                }
                else if (distanceToTarget <= chaseRadius)
                {
                    fsm?.RequestStateChange(EAnimParametor.Run);
                }

            }
        );

        // CHASE
        fsm.AddState(EAnimParametor.Run,
            onEnter: ctx => { animatorController.SetState(EAnimParametor.Run); },
            onLogic: ctx =>
            {
                if (isAttacking) return; // ✅ Không di chuyển khi tấn công

                rb.velocity = directionToTarget * mOwner.combat.agility;

                if (distanceToTarget <= attackRadius || distanceToTarget > chaseRadius)
                {
                    fsm?.RequestStateChange(EAnimParametor.Idle);
                }
            },
            onExit: ctx => rb.velocity = Vector2.zero
        );



        fsm.SetStartState(EAnimParametor.Idle);
        fsm.Init();

    }

    private void Pause() => fsm?.RequestStateChange(EAnimParametor.None);
    public void Resume() => fsm?.RequestStateChange(EAnimParametor.Idle);

    public void UpdateMovement()
    {
        directionToTarget = transOfPlayer.position - transform.position;
        distanceToTarget = directionToTarget.magnitude;
        directionToTarget = directionToTarget.normalized;

        if (directionToTarget.x != 0.0f)
        {
            spriteRenderer.flipX = directionToTarget.x < -0.01f;
        }
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
 
    public override void UpdateUI()
    {
        hp.gameObject.SetActive(true);
        hp.SetValue(mOwner.general.currVitality);
    }   
}
