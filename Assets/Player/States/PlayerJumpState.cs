using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }

    public Rigidbody rb;
    public bool isJumping;
    public float jumpMoveSpeed;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;

        SetAnimationParameters();
        LookAtMovementDirection();
        ApplyJumpForce();
        HandleAnimations();

        isJumping = true;

        if (Input.GetAxis("Sprint") > 0)
            jumpMoveSpeed = stateManager.jumpSpeedSprint;
        else
            jumpMoveSpeed = stateManager.jumpSpeedWalk;
    }

    public override void UpdateState()
    {
        if (rb.velocity.y != 0)
            stateManager.transform.Translate(stateManager.movementDirection * jumpMoveSpeed * Time.deltaTime);
        else if (!isJumping)
        {
            stateManager.movementDirection = Vector3.zero;
            stateManager.animator.speed = 1;
        }
    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Unarmed-Jump");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsJumping", true);
        stateManager.animator.SetBool("IsDodging", false);
    }

    void LookAtMovementDirection()
    {
        stateManager.movementDirection.x = Input.GetAxisRaw("Horizontal");
        stateManager.movementDirection.z = Input.GetAxisRaw("Vertical");
        stateManager.movementDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        Vector3 lookDirection = new Vector3(stateManager.movementDirection.x, 0.0f, stateManager.movementDirection.z);
        if (lookDirection != Vector3.zero)
            stateManager.animator.gameObject.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void ApplyJumpForce()
    {
        rb = stateManager.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(new Vector3(0, stateManager.jumpHeight, 0), ForceMode.Impulse);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
