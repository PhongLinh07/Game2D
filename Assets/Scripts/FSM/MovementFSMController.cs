using System.Data.Common;
using UnityEngine;
using UnityHFSM;

public class MovementFSMController : MonoBehaviour
{
    private StateMachine<EAnimParametor, EFsmAction> fsm;
    private CharacterStats charStats;
    private Rigidbody2D rb;
    private AnimatorController animatorController;
    private PlayerInputHandler playerInputHandler;
    //private bool isDash = false;

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

        fsm = new StateMachine<EAnimParametor, EFsmAction>();

        // IDLE
        fsm.AddState(EAnimParametor.Idle,
            onEnter: ctx => { animatorController.SetState(EAnimParametor.Idle); },
            onLogic: ctx => 
            {
                if (playerInputHandler.moveInput.magnitude > 0.01f) fsm?.RequestStateChange(EAnimParametor.Run);
            }
        );

        // MOVE
        fsm.AddState(EAnimParametor.Run,
            onEnter: ctx => { animatorController.SetState(EAnimParametor.Run); },
            onLogic: ctx => 
            {
                rb.velocity = playerInputHandler.moveInput * charStats.combat.agility;
                if (playerInputHandler.moveInput.magnitude < 0.01f) fsm?.RequestStateChange(EAnimParametor.Idle);
            },
            onExit: ctx => rb.velocity = Vector2.zero
        );

        // TRANSITIONS
        fsm.SetStartState(EAnimParametor.Idle);
        fsm.Init();
    }

  
    void Update()
    {
        if (Time.time >= lastDashTime + cooldownDashtime) dashBar.Full();
        fsm.OnLogic();
    }

    public void Dash()
    {
       // isDash = true;   
    }
}
