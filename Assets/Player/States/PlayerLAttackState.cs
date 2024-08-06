using RPGCharacterAnims.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLAttackState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public bool move;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        move = false;
        HandleAnimations();
        SetAnimationParameters();
        stateManager.healthSystem.ConsumeStamina(stateManager.lightAttackStamCost);
    }

    public override void UpdateState()
    {
        if (move)
        {
            stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.attackMoveSpeed * Time.deltaTime);
        }
    }

    public override void HandleInputs()
    {
    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Unarmed-Attack-L1");
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
        if (collider.gameObject.tag == "Enemy")
        {
            collider.GetComponentInParent<EnemyHealthSystem>().DoDamage(stateManager.lightAttackDamage);
            if (stateManager.healthSystem.rechargeGem)
                stateManager.healthSystem.RechargeGem(stateManager.lightAttackGemRecharge);
        }
    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
