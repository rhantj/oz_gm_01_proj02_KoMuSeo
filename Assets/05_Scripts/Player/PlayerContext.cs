using StateController;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    public Animator Anim { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public Transform PlayerCamera;

    public float MaxHP { get { return maxHp; } }
    public float CurrentHP { get { return currentHp; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float Seneitivity { get { return sensitivity; } }

    [Header("Character Stat")]
    [SerializeField] protected float maxHp;
    protected float currentHp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float sensitivity;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        InitStateMachine();
    }

    private void Start()
    {
        StateMachine?.EnterState();
    }

    private void Update()
    {
        StateMachine?.UpdateState();
    }

    private void FixedUpdate()
    {
        StateMachine?.FixedUpdateState();
    }

    public void OnUpdateStat(float maxHp, float currentHp, float moveSpeed)
    {
        this.maxHp = maxHp;
        this.currentHp = currentHp;
        this.moveSpeed = moveSpeed;
    }

    private void InitStateMachine()
    {
        PlayerController player = GetComponent<PlayerController>();
        StateMachine = new StateMachine(StateName.Move, new MoveState(player));
    }

}