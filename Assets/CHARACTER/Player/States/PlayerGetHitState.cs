using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHitState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
        SetAnimationParameters();
    }

    public override void UpdateState()
    {
        HandleInputs();
    }

    public override void HandleInputs()
    {
        if (stateManager.movementDirection.x == 0 && stateManager.movementDirection.z == 0)
            stateManager.animator.SetBool("IsMoving", false);
        else
            stateManager.animator.SetBool("IsMoving", true);
    }

    public override void HandleAnimations()
    {
        stateManager.animator.SetBool("IsHit", true);
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void HandleAudio()
    {

    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
