using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHAttackState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public bool moveForward;
    public bool moveBack;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        moveForward = false;
        HandleAnimations();
        SetAnimationParameters();
        stateManager.healthSystem.ConsumeStamina(stateManager.heavyAttackStamCost);
    }

    public override void UpdateState()
    {
        if (moveForward)
        {
            stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.attackMoveSpeed * Time.deltaTime);
        }
        else if (moveBack)
        {
            stateManager.transform.Translate(-stateManager.animator.transform.forward * stateManager.attackMoveSpeed * Time.deltaTime);
        }
    }

    public override void HandleInputs()
    {
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 0.5f;
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsLightAttack", true);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void HandleAudio()
    {

    }

    public override void OnCollisionEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.GetComponentInParent<EnemyHealthSystem>().DoDamage(stateManager.heavyAttackDamage);
            if (stateManager.healthSystem.rechargeGem)
                stateManager.healthSystem.RechargeGem(stateManager.heavyAttackGemRecharge);
        }
    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
