using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();

        stateManager.animator.speed = 1.5f;
    }

    public override void UpdateState()
    {
        HandleInputs();
        HandleAnimations();

        stateManager.transform.Translate(stateManager.movementDirection.normalized * stateManager.sprintSpeed * Time.deltaTime);
    }

    public override void HandleInputs()
    {
        stateManager.movementDirection.x = Input.GetAxisRaw("Horizontal");
        stateManager.movementDirection.z = Input.GetAxisRaw("Vertical");

        if (Input.GetAxis("Sprint") == 0)
        {
            stateManager.animator.speed = 1;
            stateManager.SwitchState(stateManager.walkState);
        }

        if (Input.GetAxis("Jump") > 0)
        {
            stateManager.SwitchState(stateManager.jumpState);
        }

        //if (Input.GetAxis("Heal") > 0)
        //    stateManager.SwitchState(stateManager.healState);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleAnimations()
    {
        LookAtMovementDirection();
        stateManager.animator.Play("Unarmed-Run-Forward");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsSprinting", true);
    }

    void LookAtMovementDirection()
    {
        stateManager.movementDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        Vector3 movement = new Vector3(stateManager.movementDirection.x, 0.0f, stateManager.movementDirection.z);
        if (movement != Vector3.zero)
            stateManager.animator.gameObject.transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
