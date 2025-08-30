using UnityEditor;
using UnityEngine;
using UnityHFSM;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemyCombat : EnemyCombatBase
{
    public float recoveryTime;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public BulletPool bulletPool;

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
                
            },
            onExit: ctx =>
            {
                _logicMonster.isAttacking = false;
            }
        );

        // TRANSITIONS
        //fsm.AddTriggerTransition(EFsmAction.Attack, new Transition<EAnimParametor>(EAnimParametor.Idle, EAnimParametor.Attack));

        SetRecoveryTime(recoveryTime);//try
    }

    public override EnemyAttackType GetAttackType() => EnemyAttackType.Ranged;

 
    // Gọi từ animation event
    public void Shoot()
    { 

        Vector3 dir = (_logicMonster.centerTarget.position - shootPoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject go = ObjectPoolManager.Instance.Spawn(EObjectPool.ProjectileChicken);
        go.GetComponent<Bullet>().Init(shootPoint.position, angle, dir, _logicMonster.mOwner.combat.atk);

    }
}
