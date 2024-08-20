using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyShootState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public AudioClip heavyShootSound;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
        SetAnimationParameters();
        stateManager.SpawnMultiProjectile();
        stateManager.healthSystem.ConsumeStamina(stateManager.lightAttackStamCost);

        stateManager.playerAudio.clip = heavyShootSound;
    }

    public override void UpdateState()
    {

    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Unarmed-Attack-L3");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 1f;
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
