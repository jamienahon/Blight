using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerSprintState sprintState = new PlayerSprintState();
    public PlayerDodgeState dodgeState = new PlayerDodgeState();
    public PlayerJumpState jumpState = new PlayerJumpState();

    [HideInInspector] public Animator animator;
    [HideInInspector] public bool isLockedOn = false;
    [HideInInspector] public bool switchStates = false;

    [HideInInspector] public Vector3 movementDirection = new Vector3();

    public float moveSpeed;
    public float sprintSpeed;
    public float jumpHeight;
    public float jumpSpeedWalk;
    public float jumpSpeedSprint;


    private void Start()
    {
        isLockedOn = false;
        currentState = idleState;
        currentState.EnterState(this);
        animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if(switchStates)
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

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(collision);
    }
}
