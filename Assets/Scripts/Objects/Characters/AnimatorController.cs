using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class AnimatorController : MonoBehaviour
{
    private LookAtMouseHandler lookAtMouseHandler;
    private PlayerInputHandler playerInputHandler;
    private SpriteRenderer spriteRenderer;
    public Vector2 directionLast = Vector2.zero;

    private float dotInputVsLook = 0.0f;
    private PlayerMovementStateID currState;
    //Controller variable use global
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        lookAtMouseHandler = GetComponent<LookAtMouseHandler>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    public void Update()
    {
        //dotInputVsLook = Vector2.Dot(lookAtMouseHandler.direction, playerInputHandler.moveInput);

        //switch (currState)
        //{
        //    case PlayerMovementStateID.Idle:
        //        PlayIfNotAlready("Idle_Right");
        //        break;

        //    case PlayerMovementStateID.Move:
        //        if (dotInputVsLook < 0.0f)
        //            PlayIfNotAlready("Run_WalkBack");
        //        else
        //            PlayIfNotAlready("Run_Right");
        //        break;

        //    case PlayerMovementStateID.Dash:
        //        if (dotInputVsLook < 0.0f)
        //            PlayIfNotAlready("Dash_Backward");
        //        else
        //            PlayIfNotAlready("Dash_Forward");
        //        break;
        //}


        dotInputVsLook = playerInputHandler.moveInput.x;

        switch (currState)
        {
            case PlayerMovementStateID.Idle: PlayIfNotAlready("Idle_Right"); break;

            case PlayerMovementStateID.Move: PlayIfNotAlready("Run_Right"); break;

            case PlayerMovementStateID.Dash:
                if (dotInputVsLook < 0.0f)
                    PlayIfNotAlready("Dash_Backward");
                else
                    PlayIfNotAlready("Dash_Forward");
                break;
        }

        if (playerInputHandler.moveInput.x != 0.0f) directionLast = playerInputHandler.moveInput;
        spriteRenderer.flipX = directionLast.x < -0.01f;
    
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





