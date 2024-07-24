using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerState currentState;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerDodgeState dodgeState = new PlayerDodgeState();

    public float moveSpeed;
    public Animator animator;
    public bool isLockedOn;

    private void Start()
    {
        isLockedOn = false;
        currentState = idleState;
        currentState.EnterState(this);

        animator = GetComponentInChildren<Animator>();
        isLockedOn = false;
    }

    private void Update()
    {
        currentState.UpdateState(this);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void SwitchState(PlayerState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(this, collision);
    }
}
