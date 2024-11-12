using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }

    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
        HandleAudio();

        stateManager.changeMovement = Time.time + Random.Range(stateManager.changeMovementRange.x, stateManager.changeMovementRange.y);
    }

    public override void UpdateState()
    {

    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {

    }

    public override void HandleAudio()
    {
        stateManager.enemyAudio.Stop();
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}
