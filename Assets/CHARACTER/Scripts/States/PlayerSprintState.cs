using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public AudioClip sprintSound;
    public bool loopSound;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
        HandleAudio();
    }

    public override void UpdateState()
    {
        HandleInputs();
        HandleAnimations();

        stateManager.healthSystem.ConsumeStamina(stateManager.sprintStamCost * Time.deltaTime);

        stateManager.transform.Translate(stateManager.movementDirection.normalized * stateManager.sprintSpeed * Time.deltaTime);
    }

    public override void HandleInputs()
    {
        stateManager.movementDirection.x = Input.GetAxisRaw("Horizontal");
        stateManager.movementDirection.z = Input.GetAxisRaw("Vertical");

        if (Input.GetAxis("Sprint") == 0)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                stateManager.SwitchState(stateManager.idleState);
            else
                stateManager.SwitchState(stateManager.walkState);
        }

        if (Input.GetAxis("Dodge") > 0 && stateManager.healthSystem.staminaBar.fillAmount >= stateManager.dodgeStamCost * (1 / stateManager.healthSystem.maxHealth))
            stateManager.SwitchState(stateManager.dodgeState);

        if (Input.GetAxis("LAttack") > 0 && stateManager.healthSystem.staminaBar.fillAmount >= stateManager.lightAttackStamCost * (1 / stateManager.healthSystem.maxHealth))
            stateManager.SwitchState(stateManager.shootState);

        if (Input.GetAxis("HAttack") > 0 && stateManager.healthSystem.staminaBar.fillAmount >= stateManager.heavyAttackStamCost * (1 / stateManager.healthSystem.maxHealth))
            stateManager.SwitchState(stateManager.heavyShootState);

        if (stateManager.healthSystem.staminaBar.fillAmount == 0)
            stateManager.SwitchState(stateManager.idleState);

        if (Input.GetAxis("Heal") > 0 && stateManager.healthSystem.healthCharges > 0)
            stateManager.SwitchState(stateManager.healState);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleAnimations()
    {
        LookAtMovementDirection();
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsWalking", false);
        stateManager.animator.SetBool("IsSprinting", true);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsAttacking", false);
        stateManager.animator.SetBool("IsHeavyAttack", false);
    }

    public override void HandleAudio()
    {
        stateManager.playerAudio.clip = sprintSound;
        stateManager.playerAudio.loop = loopSound;
        stateManager.playerAudio.Play();
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
