using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerSprintState
{
    public override PlayerStateManager stateManager { get; set; }

    public bool isParry = false;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
        SetAnimationParameters();
        stateManager.healthSystem.ConsumeStamina(stateManager.parryStamCost);
    }

    public override void UpdateState()
    {

    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Parry");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 1;
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void OnCollisionEnter(Collider collider)
    {
        //if(collider.gameObject.tag == "EnemyHit")
        //{
        //    if(isParry)
        //    {
        //        EnemyStateManager enemyStateManager = collider.GetComponentInParent<EnemyStateManager>();
        //        enemyStateManager.SwitchState(enemyStateManager.stunnedState);
        //    }
        //    else
        //    {
        //        stateManager.SwitchState(stateManager.getHitState);
        //    }
        //}
    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
