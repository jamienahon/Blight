using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        HandleInputs(stateManager);
    }

    public override void HandleInputs(PlayerStateManager stateManager)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            stateManager.SwitchState(stateManager.walkState);
        else if (Input.GetAxis("Dodge") > 0)
            stateManager.SwitchState(stateManager.dodgeState);
    }

    public override void HandleAnimations(PlayerStateManager stateManager)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager stateManager, Collision collision)
    {

    }

    public override void OnCollisionExit(PlayerStateManager stateManager, Collision collision)
    {

    }
}
