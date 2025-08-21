using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class AnimatorController : MonoBehaviour
{
    private LookAtMouseHandler lookAtMouseHandler;
    private PlayerInputHandler playerInputHandler;

    private float dotInputVsLook = 0.0f;
    private PlayerMovementStateID currState;
    //Controller variable use global
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        lookAtMouseHandler = GetComponent<LookAtMouseHandler>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    public void Update()
    {
        dotInputVsLook = Vector2.Dot(lookAtMouseHandler.direction, playerInputHandler.moveInput);

        switch (currState)
        {
            case PlayerMovementStateID.Idle:
                PlayIfNotAlready("Idle_Right");
                break;

            case PlayerMovementStateID.Move:
                if (dotInputVsLook < 0.0f)
                    PlayIfNotAlready("Run_WalkBack");
                else
                    PlayIfNotAlready("Run_Right");
                break;

            case PlayerMovementStateID.Dash:
                if (dotInputVsLook < 0.0f)
                    PlayIfNotAlready("Dash_Backward");
                else
                    PlayIfNotAlready("Dash_Forward");
                break;
        }
    }

    public void SetState(PlayerMovementStateID newState)
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





