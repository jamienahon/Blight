using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }
    public AudioClip moveSound;
    public bool loopSound;

    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
        HandleAudio();
    }

    public override void UpdateState()
    {

        stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.moveSpeed * Time.deltaTime);

        if (stateManager.IsPlayerInRange())
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {

    }

    public override void HandleAudio()
    {
        stateManager.enemyAudio.clip = moveSound;
        stateManager.enemyAudio.loop = loopSound;
        stateManager.enemyAudio.Play();
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}
