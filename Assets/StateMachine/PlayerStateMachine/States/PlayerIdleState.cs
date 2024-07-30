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
        SetAnimationParameters();
    }

    public override void UpdateState()
    {
        HandleInputs();
    }

    public override void HandleInputs()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            stateManager.SwitchState(stateManager.walkState);

        if (Input.GetAxis("Dodge") > 0)
            stateManager.SwitchState(stateManager.dodgeState);

        if (Input.GetAxis("Jump") > 0)
            stateManager.SwitchState(stateManager.jumpState);

        if (Input.GetAxis("LAttack") > 0)
            stateManager.SwitchState(stateManager.lAttackState);

        if (Input.GetAxis("HAttack") > 0)
            stateManager.SwitchState(stateManager.hAttackState);
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
