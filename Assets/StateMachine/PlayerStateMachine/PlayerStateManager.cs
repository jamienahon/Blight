using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerHealth healthSystem;

    public PlayerState currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerSprintState sprintState = new PlayerSprintState();
    public PlayerDodgeState dodgeState = new PlayerDodgeState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerLAttackState lAttackState = new PlayerLAttackState();
    public PlayerHAttackState hAttackState = new PlayerHAttackState();
    public PlayerGetHitState getHitState = new PlayerGetHitState();

    [HideInInspector] public Animator animator;
    [HideInInspector] public bool isLockedOn = false;
    [HideInInspector] public bool switchStates = false;
    [HideInInspector] public bool isInvincible = false;

    [HideInInspector] public Vector3 movementDirection = new Vector3();

    public float moveSpeed;
    public float sprintSpeed;
    public float dodgeMoveSpeed;
    public float jumpHeight;
    public float jumpSpeedWalk;
    public float jumpSpeedSprint;
    public float attackMoveAmount;


    private void Start()
    {
        isLockedOn = false;
        currentState = idleState;
        currentState.EnterState(this);
        animator = GetComponentInChildren<Animator>();
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
        if (collider.gameObject.tag == "EnemyHit" && !isInvincible)
        {
            healthSystem.DoDamage(0);
            SwitchState(getHitState);
        }
        else if(collider.gameObject.tag == "Enemy")
        {
            collider.GetComponentInParent<EnemyHealthSystem>().DoDamage(0);
        }
        //currentState.OnCollisionEnter(collider);
    }

    public void OnTriggerExit(Collider collider)
    {
        //currentState.OnCollisionExit(collider);
    }
}
