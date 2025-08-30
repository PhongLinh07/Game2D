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
        _logicMonster.fsm.AddState(EAnimParametor.Attack,
            onEnter: ctx =>
            {
                _logicMonster.animatorController.SetState(EAnimParametor.Attack);

                _logicMonster.rb.velocity = Vector2.zero;

                _logicMonster.isAttacking = true;
            },
            onLogic: ctx =>
            {
                _logicMonster.rb.velocity = isAttackPhase * _logicMonster.direction * _logicMonster.mOwner.combat.agility * 10.0f;
                PivotNormalized();
            },
            onExit: ctx =>
            {      
                isAttackPhase = 0;
                _logicMonster.isAttacking = false;
            }
        );

        SetRecoveryTime(recoveryTime);//try
    }

    public override EnemyAttackType GetAttackType() => EnemyAttackType.Melee;

    
    private void DealDamage()
    {
        isAttackPhase = 1;
  
        Collider2D hit = Physics2D.OverlapBox(pivotCenter, aoeSize, 0, LayerMask.GetMask("Player"));

        if (hit)
        {
            hit.gameObject.GetComponent<LogicUnit>().TakeDamage(_logicMonster.mOwner.combat.atk);
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
