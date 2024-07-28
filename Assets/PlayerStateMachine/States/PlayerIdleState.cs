using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;

        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void UpdateState()
    {
        HandleInputs();
    }

    public override void HandleInputs()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            stateManager.SwitchState(stateManager.walkState);
        else if (Input.GetAxis("Dodge") > 0)
            stateManager.SwitchState(stateManager.dodgeState);
        else if (Input.GetAxis("Jump") > 0)
            stateManager.SwitchState(stateManager.jumpState);
    }

    public override void HandleAnimations()
    {
        
    }

    public override void OnCollisionEnter(Collision collision)
    {

    }

    public override void OnCollisionExit(Collision collision)
    {

    }
}
