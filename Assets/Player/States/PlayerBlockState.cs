using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerState
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
        HandleInputs();

        if (stateManager.isLockedOn)
        {
            LookAtLockOnPoint();
        }
        else
        {
            LookAtMovementDirection();
        }

        stateManager.transform.Translate(stateManager.movementDirection.normalized * stateManager.blockMoveSpeed * Time.deltaTime);
    }

    public override void HandleInputs()
    {
        if (Time.time >= stateManager.endBlockPause)
        {
            stateManager.movementDirection.x = Input.GetAxisRaw("Horizontal");
            stateManager.movementDirection.z = Input.GetAxisRaw("Vertical");
        }
        else
        {
            stateManager.movementDirection.x = 0;
            stateManager.movementDirection.z = 0;
        }

        if (Input.GetAxis("Block") == 0)
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Unarmed-Attack-L2");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsJumping", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void HandleAudio()
    {

    }

    void LookAtLockOnPoint()
    {
        stateManager.movementDirection = Quaternion.Euler(0, stateManager.animator.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        stateManager.animator.transform.LookAt(Camera.main.GetComponent<CameraController>().currentLockOnPoint);
        //stateManager.animator.transform.position = new Vector3(stateManager.transform.position.x, 0, stateManager.transform.position.z);
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
