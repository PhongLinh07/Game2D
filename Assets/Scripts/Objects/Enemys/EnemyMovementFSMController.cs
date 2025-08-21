using JetBrains.Annotations;
using System;
using System.Data.Common;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityHFSM;

public class EnemyMovementFSMController : MonoBehaviour
{
    public StateMachine<EnemyMovementStateID, EnemyMovementTrigger> fsm;
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

        fsm = new StateMachine<EnemyMovementStateID, EnemyMovementTrigger>();

        fsm.AddState(EnemyMovementStateID.None);

        // NONE
        fsm.AddState(EnemyMovementStateID.None);

        // IDLE
        fsm.AddState(EnemyMovementStateID.Idle,
            onEnter: ctx => 
            { 
                animatorController.SetMovementState(EnemyMovementStateID.Idle); 
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
                    fsm.Trigger(EnemyMovementTrigger.TargetSpotted);
                }
                
            }
        );

        // CHASE
        fsm.AddState(EnemyMovementStateID.Chase,
            onEnter: ctx => { animatorController.SetMovementState(EnemyMovementStateID.Chase); },
            onLogic: ctx =>
            {
                if (objState.isAttacking) return; // ✅ Không di chuyển khi tấn công

                rb.velocity = directionToTarget * MonsterStats.combat.agility; 

                if (distanceToTarget <= attackRadius || distanceToTarget > chaseRadius)
                {
                    fsm.Trigger(EnemyMovementTrigger.TargetLost);
                }
            },
            onExit: ctx => rb.velocity = Vector2.zero
        );
 

        // TRANSITIONS
        fsm.AddTriggerTransition(EnemyMovementTrigger.TargetSpotted, new Transition<EnemyMovementStateID>(EnemyMovementStateID.Idle, EnemyMovementStateID.Chase));

        fsm.AddTriggerTransition(EnemyMovementTrigger.TargetLost, new Transition<EnemyMovementStateID>(EnemyMovementStateID.Chase, EnemyMovementStateID.Idle));

        fsm.AddTriggerTransitionFromAny(EnemyMovementTrigger.None, new Transition<EnemyMovementStateID>(EnemyMovementStateID.Any, EnemyMovementStateID.None));

        fsm.AddTriggerTransition(EnemyMovementTrigger.TargetLost, new Transition<EnemyMovementStateID>(EnemyMovementStateID.None, EnemyMovementStateID.Idle));


        fsm.SetStartState(EnemyMovementStateID.Idle);
        fsm.Init();
    }

    private void Pause() => fsm.Trigger(EnemyMovementTrigger.None);
    public void Resume() => fsm.Trigger(EnemyMovementTrigger.TargetLost);

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
