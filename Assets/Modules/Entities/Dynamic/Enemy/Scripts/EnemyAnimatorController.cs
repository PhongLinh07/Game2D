using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;



public class EnemyAnimatorController : MonoBehaviour
{
  
    private EAnimParametor currState;

    //Controller variable use global
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        switch (currState)
        {
            case EAnimParametor.Idle:
                PlayIfNotAlready("Idle_Right");
                break;

            case EAnimParametor.Run:
                PlayIfNotAlready("Run_Right");
                break;
            case EAnimParametor.Attack:
                PlayIfNotAlready("Attack_Right");
                break;
        }
    }


    public void SetState(EAnimParametor newState)
    {
        this.currState = newState;
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





