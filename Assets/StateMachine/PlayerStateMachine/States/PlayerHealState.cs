using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();

        stateManager.endHeal = Time.time + stateManager.lengthOfHeal;
    }

    public override void UpdateState()
    {
        HandleAnimations();

        if (Time.time >= stateManager.endHeal)
            stateManager.SwitchState(stateManager.idleState);

        stateManager.transform.Translate(stateManager.movementDirection.normalized * stateManager.healingMoveSpeed * Time.deltaTime);
    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {
        if (stateManager.isLockedOn)
        {
            LookAtLockOnPoint();
            stateManager.animator.SetFloat("HorizontalMovement", Input.GetAxis("Horizontal"));
            stateManager.animator.SetFloat("VerticalMovement", Input.GetAxis("Vertical"));
        }
        else
        {
            LookAtMovementDirection();
            stateManager.animator.Play("Unarmed-Run-Forward");
        }
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 0.75f;
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsJumping", false);
    }

    void LookAtLockOnPoint()
    {
        stateManager.movementDirection = Quaternion.Euler(0, stateManager.animator.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        stateManager.animator.transform.LookAt(Camera.main.GetComponent<CameraController>().currentLockOnPoint);
        stateManager.animator.transform.position = new Vector3(stateManager.transform.position.x, 0, stateManager.transform.position.z);
        stateManager.animator.transform.rotation = Quaternion.Euler(0, stateManager.animator.transform.rotation.eulerAngles.y, 0);
    }

    void LookAtMovementDirection()
    {
        stateManager.movementDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        Vector3 lookDirection = new Vector3(stateManager.movementDirection.x, 0.0f, stateManager.movementDirection.z);
        if (lookDirection != Vector3.zero)
            stateManager.animator.gameObject.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
