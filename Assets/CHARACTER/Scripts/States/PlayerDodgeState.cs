using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DodgeDirection
{
    forward,
    back,
    left,
    right
}

public class PlayerDodgeState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public AudioClip dodgeSound;
    public bool loopSound;
    public bool move;
    public bool check = false;
    DodgeDirection dodgeDir;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
        HandleAudio();

        HandleAnimations();
        stateManager.healthSystem.ConsumeStamina(stateManager.dodgeStamCost);

        if (Input.GetAxis("Horizontal") > 0)
            dodgeDir = DodgeDirection.right;
        else if(Input.GetAxis("Horizontal") < 0)
            dodgeDir = DodgeDirection.left;
        else if (Input.GetAxis("Vertical") > 0)
            dodgeDir = DodgeDirection.forward;
        else if (Input.GetAxis("Vertical") < 0)
            dodgeDir = DodgeDirection.back;
    }

    public override void UpdateState()
    {
        if (move)
            stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.dodgeMoveSpeed * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            stateManager.animator.SetBool("IsSprinting", false);
            stateManager.animator.SetBool("IsWalking", false);
        }
        else if (Input.GetAxis("Sprint") > 0)
        {
            stateManager.animator.SetBool("IsSprinting", true);
            stateManager.animator.SetBool("IsWalking", false);
        }
        else
        {
            stateManager.animator.SetBool("IsSprinting", false);
            stateManager.animator.SetBool("IsWalking", true);
        }

        if(check)
            if(Input.GetAxis("LAttack") > 0)
            {
                if (dodgeDir == DodgeDirection.forward)
                    stateManager.animator.SetFloat("WS", 1);
                else if (dodgeDir == DodgeDirection.back)
                    stateManager.animator.SetFloat("WS", -1);
                if (dodgeDir == DodgeDirection.right)
                    stateManager.animator.SetFloat("AD", 1);
                if (dodgeDir == DodgeDirection.left)
                    stateManager.animator.SetFloat("AD", -1);
            }
    }

    public override void HandleInputs()
    {
        
    }

    public override void HandleAnimations()
    {
        LookAtDodgeDirection();
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 1;
        stateManager.animator.SetBool("IsWalking", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsAttacking", false);
        stateManager.animator.SetBool("IsDodging", true);
        stateManager.animator.SetBool("IsCombo", false);
        stateManager.animator.SetBool("IsHeavyAttack", false);
        stateManager.animator.SetBool("IsHealing", false);
        stateManager.animator.SetFloat("WS", 0);
        stateManager.animator.SetFloat("AD", 0);
    }

    public override void HandleAudio()
    {
        stateManager.playerAudio.clip = dodgeSound;
        stateManager.playerAudio.loop = loopSound;
        stateManager.playerAudio.Play();
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
