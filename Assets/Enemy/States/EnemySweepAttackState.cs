using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySweepAttackState : EnemyState
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
        stateManager.animator.Play("SweepAttack");
    }

    public override void SetAnimationParameters()
    {

    }

    public override void HandleAudio()
    {

    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}