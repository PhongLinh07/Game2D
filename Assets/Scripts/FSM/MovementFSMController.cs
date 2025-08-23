using System.Data.Common;
using UnityEngine;
using UnityHFSM;

public class MovementFSMController : MonoBehaviour
{
    private StateMachine<PlayerMovementStateID, PlayerMovementTrigger> fsm;
    private CharacterStats charStats;
    private Rigidbody2D rb;
    private AnimatorController animatorController;
    private PlayerInputHandler playerInputHandler;
    private bool isDash = false;

    public float dashDuration = 0.2f;
    public float lastDashTime = -9999;
    public float cooldownDashtime = 1.0f;

    public GameObject dashFX_Prefab;
    private DashFXController dashFX_Controller;
    public StatusBar dashBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        charStats = GetComponent<CharacterStats>();
        animatorController = GetComponent<AnimatorController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        dashFX_Controller = dashFX_Prefab.GetComponent<DashFXController>();

        fsm = new StateMachine<PlayerMovementStateID, PlayerMovementTrigger>();

        // IDLE
        fsm.AddState(PlayerMovementStateID.Idle,
            onEnter: ctx => { animatorController.SetState(PlayerMovementStateID.Idle); },
            onLogic: ctx => 
            {
                if (playerInputHandler.moveInput.magnitude > 0.01f) fsm.Trigger(PlayerMovementTrigger.MoveInput);
            }
        );

        // MOVE
        fsm.AddState(PlayerMovementStateID.Move,
            onEnter: ctx => { animatorController.SetState(PlayerMovementStateID.Move); },
            onLogic: ctx => 
            {
                rb.velocity = playerInputHandler.moveInput * charStats.combat.agility;
                if (playerInputHandler.moveInput.magnitude < 0.01f) fsm.Trigger(PlayerMovementTrigger.NoInput);
                else if (isDash && Time.time >= lastDashTime + cooldownDashtime) fsm.Trigger(PlayerMovementTrigger.DashPressed);
            },
            onExit: ctx => rb.velocity = Vector2.zero
        );

        // DASH
        fsm.AddState(PlayerMovementStateID.Dash,
            onEnter: ctx => 
            {
                animatorController.SetState(PlayerMovementStateID.Dash);
                dashFX_Controller.Dashing(transform.position, playerInputHandler.moveInput);
                lastDashTime= Time.time;
                dashBar.Zero();
            },
            onLogic: ctx => 
            {
                rb.velocity = playerInputHandler.moveInput.normalized * charStats.combat.agility * 2;
                if (Time.time >= lastDashTime + dashDuration)
                {
                    // Đợi một frame mới gọi Trigger để đảm bảo chuyển mượt
                    fsm.Trigger(PlayerMovementTrigger.DashFinished);
                }
            },
            onExit: ctx =>
            {
                rb.velocity = Vector2.zero;
                isDash = false;
            }
        );

        // TRANSITIONS
        fsm.AddTriggerTransition(PlayerMovementTrigger.MoveInput, new Transition<PlayerMovementStateID>(PlayerMovementStateID.Idle, PlayerMovementStateID.Move));

        fsm.AddTriggerTransition(PlayerMovementTrigger.NoInput,new Transition<PlayerMovementStateID>(PlayerMovementStateID.Move, PlayerMovementStateID.Idle));

        fsm.AddTriggerTransition(PlayerMovementTrigger.DashPressed, new Transition<PlayerMovementStateID>(PlayerMovementStateID.Move, PlayerMovementStateID.Dash));

        fsm.AddTriggerTransition(PlayerMovementTrigger.DashFinished, new Transition<PlayerMovementStateID>(PlayerMovementStateID.Dash, PlayerMovementStateID.Idle));

        fsm.SetStartState(PlayerMovementStateID.Idle);
        fsm.Init();
    }

  
    void Update()
    {
        if (Time.time >= lastDashTime + cooldownDashtime) dashBar.Full();
        fsm.OnLogic();
    }

    public void Dash()
    {
        isDash = true;   
    }
}
