using UnityEngine;

public enum EnemyType { Boar, Chicken }

public class EnemyAIController : MonoBehaviour
{
    public EnemyType type;
    public GameObject currentTarget;
    public UnitStats stats;

    private EnemyCombatBase combatSystem;

    public EnemyMovementFSMController movementFSM;
    public GameObject imageDie;
    private SpriteRenderer spriteRenderer;

    private HPController hpController;
    private bool isAttacking;


    void Start()
    {
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
        hpController = GetComponent<HPController>();
        combatSystem.Init(this);
    }

    void Update()
    {
        if (!hpController.isAlive)
        {
            GameObject go = Instantiate(imageDie, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
            Destroy(gameObject);
            return;
        }

        if (!isAttacking)
        {
            if (!combatSystem.Recovered()) return;

            movementFSM.UpdateMovement();
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
        movementFSM.Resume();
    }

    
}
