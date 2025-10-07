using UnityEngine;
using UnityHFSM;

public abstract class EnemyCombatBase : MonoBehaviour, IEnemyCombat
{

    private float _lastAttackTime;
    protected float _recoveryTime;
    protected LogicMonster _logicMonster;

    public void Init(LogicMonster logicMonster)
    {
        _logicMonster = logicMonster;
        _lastAttackTime = 0.0f;
        _recoveryTime = 0.0f;
        SetupFSM();
    }

    public void SetRecoveryTime(float time)
    {
        _recoveryTime = time;
    }


    public virtual void ResetCombat()
    {
        _logicMonster.fsm?.RequestStateChange(EAnimParametor.Idle);
        _lastAttackTime = Time.time;
    }

    public bool Recovered()
    {
        return Time.time >= _lastAttackTime + _recoveryTime;
    }

    protected abstract void SetupFSM();
    public abstract EnemyAttackType GetAttackType();
}
