using JetBrains.Annotations;
using System;
using System.Data.Common;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityHFSM;

public class EnemyMovementFSMController : MonoBehaviour
{
    public StateMachine<EAnimParametor, EFsmAction> fsm;
    private MonsterStats MonsterStats;
    private ObjectState objState;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private EnemyAnimatorController animatorController;
    private EnemyAIController enemyAIController;

    public Vector2 directionToTarget = Vector2.zero;
    private float distanceToTarget = 100.0f;


    public Transform transOfPlayer;
    public float chaseRadius;
    public float attackRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        MonsterStats = GetComponent<MonsterStats>();
        animatorController = GetComponent<EnemyAnimatorController>();
        objState = GetComponent<ObjectState>();
        enemyAIController = GetComponent<EnemyAIController>();

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
                if (objState.isAttacking) return;

                if (distanceToTarget <= attackRadius)
                {
                    objState.lookDirection = directionToTarget;
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
                if (objState.isAttacking) return; // ✅ Không di chuyển khi tấn công

                rb.velocity = directionToTarget * MonsterStats.combat.agility; 

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
}
