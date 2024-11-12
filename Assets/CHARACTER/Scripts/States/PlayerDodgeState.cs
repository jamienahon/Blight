using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public AudioClip dodgeSound;
    public bool loopSound;
    public bool move;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
        HandleAudio();

        HandleAnimations();
        stateManager.healthSystem.ConsumeStamina(stateManager.dodgeStamCost);
    }

    public override void UpdateState()
    {
        if (move)
            stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.dodgeMoveSpeed * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            stateManager.animator.SetBool("IsMoving", false);
        else
            stateManager.animator.SetBool("IsMoving", true);
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
        stateManager.animator.speed = 1f;
        stateManager.animator.SetLayerWeight(1, 0);
        stateManager.animator.SetBool("IsDodging", true);
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
