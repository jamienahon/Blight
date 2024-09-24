using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }

    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();

        stateManager.endStun = Time.time + stateManager.stunnedLength;
    }

    public override void UpdateState()
    {
        if(Time.time >= stateManager.endStun)
        {
            stateManager.animator.SetBool("IsStunned", false);
            stateManager.SwitchState(stateManager.idleState);
        }
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsStunned", true);
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
