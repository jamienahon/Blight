using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }

    public Rigidbody rb;
    public bool isJumping;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        isJumping = true;

        rb = stateManager.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(new Vector3(0, stateManager.jumpHeight, 0), ForceMode.Impulse);

        HandleAnimations();

        //stateManager.movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public override void UpdateState()
    {
        if (rb.velocity.y != 0)
            stateManager.transform.Translate(stateManager.movementDirection * stateManager.moveSpeed * Time.deltaTime);
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

    public override void OnCollisionEnter(Collision collision)
    {

    }

    public override void OnCollisionExit(Collision collision)
    {

    }
}
