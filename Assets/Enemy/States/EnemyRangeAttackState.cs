using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }

    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
    }

    public override void UpdateState()
    {

    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Unarmed-Attack-L1 1");
    }

    public override void SetAnimationParameters()
    {

    }

    public override void HandleAuido()
    {

    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}
