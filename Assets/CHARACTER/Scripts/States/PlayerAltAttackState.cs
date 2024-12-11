using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAltAttackState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public bool move;
    public bool moveBack;
    public bool moveLeft;
    public bool moveRight;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
        LookAtCameraDirection();
        DecideAttack();
    }

    public override void UpdateState()
    {
        if (move)
            stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.dodgeMoveSpeed * Time.deltaTime);
        else if(moveBack)
            stateManager.transform.Translate(-stateManager.animator.transform.forward * stateManager.dodgeMoveSpeed * Time.deltaTime);
        else if (moveRight)
            stateManager.transform.Translate(stateManager.animator.transform.right * stateManager.slowSpeed * Time.deltaTime);
        else if (moveLeft)
            stateManager.transform.Translate(-stateManager.animator.transform.right * stateManager.slowSpeed * Time.deltaTime);
    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 1;
        stateManager.animator.SetBool("IsWalking", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsAttacking", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsHeavyAttack", false);
    }

    public override void HandleAudio()
    {

    }

    void DecideAttack()
    {
        if (stateManager.animator.GetFloat("AD") == 0 && stateManager.animator.GetFloat("WS") == 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
                stateManager.animator.SetFloat("AD", 1);
            else if (Input.GetAxis("Horizontal") < 0)
                stateManager.animator.SetFloat("AD", -1);
            else if (Input.GetAxis("Vertical") > 0)
                stateManager.animator.SetFloat("WS", 1);
            else if (Input.GetAxis("Vertical") < 0)
                stateManager.animator.SetFloat("WS", -1);
        }
    }

    void LookAtCameraDirection()
    {
        stateManager.animator.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
