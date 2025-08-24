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
    private EAnimParametor currState;
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
        dotInputVsLook = playerInputHandler.moveInput.x;

        switch (currState)
        {
            case EAnimParametor.Idle: PlayIfNotAlready("Idle_Right"); break;

            case EAnimParametor.Run: PlayIfNotAlready("Run_Right"); break;
        }

        if (playerInputHandler.moveInput.x != 0.0f) directionLast = playerInputHandler.moveInput;
        spriteRenderer.flipX = directionLast.x < -0.01f;
    
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





