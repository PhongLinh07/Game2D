using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityHFSM;
using static UnityEngine.UI.CanvasScaler;

public class LogicMonster : LogicUnit
{
    public MonsterUnit mOwner;
    public StateMachine<EAnimParametor, EFsmAction> fsm;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public EnemyAnimatorController animatorController;


    private EnemyCombatBase combatSystem;
    [SerializeField] private GameObject imageDie;

    public Vector2 directionToTarget = Vector2.zero;
    private float distanceToTarget = 100.0f;


    public Transform currentTarget; //vị trí đứng
    public Transform centerTarget; //vị trí tim 
    public Vector2 lockTarget; // hướng tấn công cuối

    public float chaseRadius;
    public float attackRadius;

    public StatusBar hp;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mOwner = GetComponent<MonsterUnit>();
        animatorController = GetComponent<EnemyAnimatorController>();

        hp.Init(mOwner.general.vitality, mOwner.general.vitality);
        hp.gameObject.SetActive(false);

        Bootstrapper.Instance.eventWhenCloneCharacter += Init;
    }
  
    private void Init(LogicCharacter logicCharacter)
    {
        Bootstrapper.Instance.eventWhenCloneCharacter -= Init;
        currentTarget = logicCharacter.transBottom;
        centerTarget = logicCharacter.transCenter;

        InitData();

        combatSystem = GetComponent<EnemyCombatBase>();
        combatSystem.Init(this);
        
        Inited = true;
        fsm.SetStartState(EAnimParametor.Idle);
        fsm.Init();
    }
    public override void InitData()
    {        
        fsm = new StateMachine<EAnimParametor, EFsmAction>();

        // IDLE
        fsm.AddState(EAnimParametor.Idle,
            onEnter: ctx =>
            {
                animatorController.SetState(EAnimParametor.Idle);
                rb.velocity = Vector2.zero;
            },
            onLogic: ctx =>
            {
                if (distanceToTarget <= attackRadius)
                {
                    if (!combatSystem.Recovered()) return;
  
                    direction = directionToTarget;

                    fsm?.RequestStateChange(EAnimParametor.Attack);
  
                }
                else if (distanceToTarget <= chaseRadius )
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

   
    private void Update()
    {
        if(!Inited) return;


        if(!isAttacking && combatSystem.Recovered())
        {
            directionToTarget = currentTarget.position - transform.position;
            distanceToTarget = directionToTarget.magnitude;
            directionToTarget = directionToTarget.normalized;


            if (directionToTarget.x != 0.0f)
            {
                spriteRenderer.flipX = directionToTarget.x < -0.01f;
            }
        }
        
        fsm.OnLogic();
       
    }


    public override void TakeDamage(int damage)
    {
        mOwner.TakeDamage(damage);
        UpdateUI();
        if (hp.currValue <= 0)
        {
            GameObject go = Instantiate(imageDie, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
            Destroy(gameObject);
            return;
        }
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
