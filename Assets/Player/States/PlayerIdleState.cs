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
        if (stateManager.isLockedOn)
            LookAtLockOnPoint();
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
        stateManager.animator.speed = 1;
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
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