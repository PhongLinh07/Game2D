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
                
            },
            onExit: ctx =>
            {
                rb.velocity = Vector2.zero; // đảm bảo đứng yên

                ai.HandleAttackFinished();
            }
        );

        // TRANSITIONS
        //fsm.AddTriggerTransition(EFsmAction.Attack, new Transition<EAnimParametor>(EAnimParametor.Idle, EAnimParametor.Attack));




        fsm.SetStartState(EAnimParametor.None);
        fsm.Init();

        //ký sinh tạm thời 
        SetRecoveryTime(recoveryTime);
    }

    public override EnemyAttackType GetAttackType() => EnemyAttackType.Ranged;

    public void DoShootIfReady()
    {
        if (!Recovered()) return;

        fsm?.RequestStateChange(EAnimParametor.Attack);
    }

    // Gọi từ animation event
    public void Shoot()
    {
        Vector3 dir = (ai.currentTarget.transform.position - shootPoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject go = ObjectPoolManager.Instance.Spawn(EObjectPool.ProjectileChicken);
        go.GetComponent<Bullet>().Init(shootPoint.position, angle, dir, ai.stats.combat.atk);

    }
}
