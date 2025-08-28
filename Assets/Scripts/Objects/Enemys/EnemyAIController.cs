using UnityEngine;

public enum EnemyType { Boar, Chicken }

public class EnemyAIController : MonoBehaviour
{
    public EnemyType type;
    public GameObject currentTarget;
    public MonsterUnit mOwner;
    private EnemyCombatBase combatSystem;

    public LogicMonster logicMonster;
    public GameObject imageDie;
    private SpriteRenderer spriteRenderer;

    private bool isAttacking;


    void Start()
    {
        mOwner = GetComponent<MonsterUnit>();

        // Gán combat system tuỳ theo loại
        switch (type)
        {
            case EnemyType.Boar:
                combatSystem = GetComponent<MeleeEnemyCombat>();
                break;
            case EnemyType.Chicken:
                combatSystem = GetComponent<RangedEnemyCombat>();
                break;
        }

        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        combatSystem.Init(this);

        
    }

    void Update()
    {
        if (logicMonster.hp.currValue <= 0)
        {
            GameObject go = Instantiate(imageDie, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
            Destroy(gameObject);
            return;
        }

        if (!isAttacking)
        {
            if (!combatSystem.Recovered()) return;

            logicMonster.UpdateMovement();
        }
        else
        {
            combatSystem.UpdateCombat();
        }
    }

    public void OnEnterAttackRange()
    {
        if (combatSystem is MeleeEnemyCombat melee) melee.DoAttackIfReady();
        else if (combatSystem is RangedEnemyCombat ranged) ranged.DoShootIfReady();
    }

    public void HandleAttackRequest()
    {
        if (!combatSystem.Recovered()) return;
        isAttacking = true;
        OnEnterAttackRange();
    }

    public void HandleAttackFinished()
    {
        isAttacking = false;
        logicMonster.Resume();
    }

    
}
