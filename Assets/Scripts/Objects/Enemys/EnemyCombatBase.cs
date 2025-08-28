using UnityEngine;
using UnityHFSM;

public abstract class EnemyCombatBase : MonoBehaviour, IEnemyCombat
{
    protected EnemyAIController ai;
    protected Rigidbody2D rb;
    protected StateMachine<EAnimParametor, EFsmAction> fsm;
    private float _lastAttackTime;
    protected float _recoveryTime;
    protected EnemyAnimatorController animatorController;
    protected LogicMonster logicMonster;

    public virtual void Init(EnemyAIController aiController)
    {
        ai = aiController;
        rb = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<EnemyAnimatorController>();
        logicMonster = GetComponent<LogicMonster>();

        _lastAttackTime = 0.0f;
        _recoveryTime = 0.0f;
        SetupFSM();
    }

    public void SetRecoveryTime(float time)
    {
        _recoveryTime = time;
    }

    public virtual void UpdateCombat()
    {
        fsm?.OnLogic();
    }

    public virtual void ResetCombat()
    {
        fsm?.RequestStateChange(EAnimParametor.None);
        _lastAttackTime = Time.time;
    }

    public bool Recovered()
    {
        return Time.time >= _lastAttackTime + _recoveryTime;
    }

    protected abstract void SetupFSM();
    public abstract EnemyAttackType GetAttackType();
}
