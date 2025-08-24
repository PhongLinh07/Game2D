using UnityEngine;
using UnityEngine.UIElements;
using UnityHFSM;

public class MeleeEnemyCombat : EnemyCombatBase
{
    private int isAttackPhase = 0;

    private Vector2 aoeSize = Vector2.zero;
    private Vector2 pivotCenter = Vector2.zero;
    private BoxCollider2D box2D;
    public float recoveryTime;

    private void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
    }
    protected override void SetupFSM()
    {
        fsm = new StateMachine<EAnimParametor, EFsmAction>();

        // IDLE
        fsm.AddState(EAnimParametor.None);

        fsm.AddState(EAnimParametor.Attack,
            onEnter: ctx =>
            {
                animatorController.SetState(EAnimParametor.Attack);

                rb.velocity = Vector2.zero;
            },
            onLogic: ctx =>
            {
                rb.velocity = isAttackPhase * objState.lookDirection * ai.stats.combat.agility * 7.0f;
                PivotNormalized();
            },
            onExit: ctx =>
            {
                rb.velocity = Vector2.zero; // đảm bảo đứng yên

                isAttackPhase = 0; 
                ai.HandleAttackFinished();
            }
        );

        
        fsm.SetStartState(EAnimParametor.None);
        fsm.Init();

        //ký sinh tạm thời 
        SetRecoveryTime(recoveryTime);
    }

    public override EnemyAttackType GetAttackType() => EnemyAttackType.Melee;

    public void DoAttackIfReady()
    {
        if (!Recovered()) return;

        fsm?.RequestStateChange(EAnimParametor.Attack);
    }

    private void DealDamage()
    {
        isAttackPhase = 1;
  
        Collider2D hit = Physics2D.OverlapBox(pivotCenter, aoeSize, 0, LayerMask.GetMask("Player"));

        if (hit)
        {
            hit.gameObject.GetComponent<HPController>().TakeDamage(ai.stats.combat.atk);
        }

    }


    private void PivotNormalized() //Try
    {
        pivotCenter = transform.TransformPoint(box2D.offset);
        aoeSize = Vector2.Scale(box2D.size, transform.lossyScale);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pivotCenter, aoeSize);
    }

}
