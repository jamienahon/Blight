using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState;

    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerHealthSystem healthSystem;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerSprintState sprintState = new PlayerSprintState();
    public PlayerDodgeState dodgeState = new PlayerDodgeState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerLAttackState lAttackState = new PlayerLAttackState();
    public PlayerHAttackState hAttackState = new PlayerHAttackState();
    public PlayerGetHitState getHitState = new PlayerGetHitState();

    [HideInInspector] public bool isLockedOn = false;
    [HideInInspector] public bool switchStates = false;
    [HideInInspector] public bool isInvincible = false;

    [HideInInspector] public Vector3 movementDirection = new Vector3();

    [Header("Movement")]
    public float runSpeed;
    public float sprintSpeed;
    public float sprintStamCost;

    [Header("Dodging")]
    public float dodgeMoveSpeed;
    public float dodgeStamCost;

    [Header("Jumping")]
    public float jumpHeight;
    public float jumpSpeedWalk;
    public float jumpSpeedSprint;
    public float jumpStamCost;

    [Header("Attacking")]
    public float attackMoveSpeed;

    [Header("Light Attack")]
    public float lightAttackDamage;
    public float lightAttackStamCost;
    public float lightAttackGemRecharge;

    [Header("Heavy Attack")]
    public float heavyAttackDamage;
    public float heavyAttackStamCost;
    public float heavyAttackGemRecharge;


    private void Start()
    {
        isLockedOn = false;
        currentState = idleState;
        currentState.EnterState(this);
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<PlayerHealthSystem>();
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
