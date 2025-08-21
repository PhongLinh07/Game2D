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
        fsm = new StateMachine<EnemyCombatStateID, EnemyCombatTrigger>();

        // IDLE
        fsm.AddState(EnemyCombatStateID.None);

        fsm.AddState(EnemyCombatStateID.BaseAttack,
            onEnter: ctx =>
            {
                animatorController.SetCombatState(EnemyCombatStateID.BaseAttack);
                rb.velocity = Vector2.zero;
            },
            onLogic: ctx =>
            {
                // rb.velocity = isAttackPhase * objState.lookDirection * monsterStats.combat.agility * 7.0f;
            },
            onExit: ctx =>
            {
                rb.velocity = Vector2.zero; // đảm bảo đứng yên

                ai.HandleAttackFinished();
            }
        );

        // TRANSITIONS
        fsm.AddTriggerTransition(EnemyCombatTrigger.InRange, new Transition<EnemyCombatStateID>(EnemyCombatStateID.None, EnemyCombatStateID.BaseAttack));

        fsm.AddTriggerTransition(EnemyCombatTrigger.OutOfRange, new Transition<EnemyCombatStateID>(EnemyCombatStateID.BaseAttack, EnemyCombatStateID.None));


        fsm.SetStartState(EnemyCombatStateID.None);
        fsm.Init();

        //ký sinh tạm thời 
        SetRecoveryTime(recoveryTime);
    }

    public override EnemyAttackType GetAttackType() => EnemyAttackType.Ranged;

    public void DoShootIfReady()
    {
        if (!Recovered()) return;

        fsm.Trigger(EnemyCombatTrigger.InRange);
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
