using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();

        HandleAnimations();
    }

    public override void UpdateState()
    {
        stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.dodgeMoveSpeed * Time.deltaTime);
    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {
        LookAtDodgeDirection();
        stateManager.animator.Play("Unarmed-DiveRoll-Forward1");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsDodging", true);
        stateManager.animator.SetBool("IsJumping", false);
    }

    void LookAtDodgeDirection()
    {
        Vector3 movDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 dodgeDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movDirection;
        if (dodgeDirection != Vector3.zero)
            stateManager.animator.transform.rotation = Quaternion.LookRotation(dodgeDirection);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
