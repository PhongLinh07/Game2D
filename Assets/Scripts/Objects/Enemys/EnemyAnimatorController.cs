using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;



public class EnemyAnimatorController : MonoBehaviour
{
  
    private EnemyMovementStateID currMoveState;
    private EnemyCombatStateID currCombatState;

    //Controller variable use global
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        MovementAnimation();
        CombatAnimation();
    }

    private void MovementAnimation()
    {
        switch (currMoveState)
        {
            case EnemyMovementStateID.Idle:
                PlayIfNotAlready("Idle_Right");
                break;

            case EnemyMovementStateID.Chase:
                PlayIfNotAlready("Run_Right");
                break;
        }
    }

    private void CombatAnimation()
    {
        switch (currCombatState)
        {
            case EnemyCombatStateID.BaseAttack:
                PlayIfNotAlready("Attack_Right");
                break;
        }
    }

    public void SetMovementState(EnemyMovementStateID newState)
    {
        currMoveState = newState;
        currCombatState = EnemyCombatStateID.None;
    }

    public void SetCombatState(EnemyCombatStateID newState)
    {
        {
            currMoveState = EnemyMovementStateID.None;
            currCombatState = newState;
        }
    }


    void PlayIfNotAlready(string clipName)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(clipName))
            animator.Play(clipName);
    }

    public bool ClipIsPlay(string tag)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Clip is Plaiing
        if (stateInfo.IsTag(tag) && stateInfo.normalizedTime < 1f)
        {
            return true; 
        }

        // Clip don't play
        return false; 
    }

}





