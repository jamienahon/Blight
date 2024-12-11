using UnityEngine;


public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState;

    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerHealthSystem healthSystem;
    [HideInInspector] public AudioSource playerAudio;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerSprintState sprintState = new PlayerSprintState();
    public PlayerDodgeState dodgeState = new PlayerDodgeState();
    public PlayerLAttackState lAttackState = new PlayerLAttackState();
    public PlayerHAttackState hAttackState = new PlayerHAttackState();
    public PlayerGetHitState getHitState = new PlayerGetHitState();
    public PlayerShootState shootState = new PlayerShootState();
    public PlayerHeavyShootState heavyShootState = new PlayerHeavyShootState();
    public PlayerHealState healState = new PlayerHealState();
    public PlayerAltAttackState altAttackState = new PlayerAltAttackState();

    [HideInInspector] public bool isLockedOn = false;
    [HideInInspector] public bool switchStates = false;
    [HideInInspector] public bool isInvincible = false;

    [HideInInspector] public Vector3 movementDirection = new Vector3();

    [Header("Movement")]
    public float runSpeed;
    public float sprintSpeed;
    public float sprintStamCost;
    public float healingMoveSpeed;

    [Header("Dodging")]
    public float dodgeMoveSpeed;
    public float dodgeStamCost;
    public float slowSpeed;

    [Header("Attacking")]
    public float arrowTrackingStrength;
    public float lockedOnArrowTrackingStrength;
    public float arrowTrackingRange;
    public float attackMoveSpeed;
    public float arrowMoveSpeed;
    public float damageFalloff;
    public bool allowMovementWhileAttacking;
    public float attackMovementSpeed;
    public float arrowLifetime;

    [Header("Light Attack")]
    public GameObject projectile;
    public float lightAttackDamage;
    public float lightAttackStamCost;
    public float lightAttackGemRecharge;

    [Header("Heavy Attack")]
    public GameObject heavyProjectile;
    public float heavyAttackDamage;
    public float heavyAttackStamCost;
    public float heavyAttackGemRecharge;
    public float heavyAttackMoveSpeed;

    [Header("Healing")]
    public float healLengthSeconds;
    public float healAmount;

    private void Start()
    {
        isLockedOn = false;
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<PlayerHealthSystem>();
        playerAudio = GetComponent<AudioSource>();

        currentState = idleState;
        currentState.EnterState(this);
    }

    public void FixedUpdate()
    {
        if (switchStates)
        {
            currentState.EnterState(this);
            switchStates = false;
        }
        currentState.UpdateState();
    }

    public void SwitchState(PlayerState state)
    {
        currentState = state;
        switchStates = true;
    }

    public void OnTriggerEnter(Collider collider)
    {
        currentState.OnCollisionEnter(collider);
    }

    public void OnTriggerExit(Collider collider)
    {

    }
}
