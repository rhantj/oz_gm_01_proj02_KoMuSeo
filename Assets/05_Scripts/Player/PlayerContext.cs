using StateController;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public StateMachine MovementSM { get; private set; }
    public StateMachine ActionSM { get; private set; }
    public Animator Anim { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public Transform PlayerCamera;

    public float MaxHP { get { return maxHp; } }
    public float CurrentHP { get { return currentHp; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
    public float Seneitivity { get { return sensitivity; } set { sensitivity = value; } }

    [Header("Character Stat")]
    [SerializeField] protected float maxHp;
    protected float currentHp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float sensitivity;

    private PlayerController player;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        player = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        InitMovementStateMachine();
    }

    private void Start()
    {
        MovementSM?.EnterState();
        ActionSM?.EnterState();
    }

    private void Update()
    {
        MovementSM?.UpdateState();
        ActionSM?.UpdateState();
    }

    private void FixedUpdate()
    {
        MovementSM?.FixedUpdateState();
        ActionSM?.FixedUpdateState();
    }

    public void OnUpdateStat(float maxHp, float currentHp, float moveSpeed)
    {
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.moveSpeed = moveSpeed;
    }

    private void InitMovementStateMachine()
    {
        MovementSM = new StateMachine(StateName.Idle, new IdleState(player));
        MovementSM.AddState(StateName.Move, new MoveState(player));
        MovementSM.AddState(StateName.Jump, new JumpState(player));
        MovementSM.AddState(StateName.Crouch, new CrouchState(player));
    }

    private void InitActionStateMachine()
    {
        ActionSM = new StateMachine(StateName.Fire, new FireState(player));
        ActionSM.AddState(StateName.Reload, new ReloadState(player));
        ActionSM.AddState(StateName.Melee, new MeleeState(player));
        ActionSM.AddState(StateName.Throw, new ThrowState(player));
    }
}