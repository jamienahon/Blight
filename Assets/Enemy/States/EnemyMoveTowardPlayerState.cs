using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMoveTowardPlayerState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }
    float changeState;

    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        changeState = Time.time + stateManager.timeToGetInRange;
    }

    public override void UpdateState()
    {
        stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.fastMoveSpeed * Time.deltaTime);

        if (stateManager.IsPlayerInRange())
        {
            if (stateManager.desiredAttack == Attacks.SlashAttack)
                stateManager.SwitchState(stateManager.slashAttackState);
            else if (stateManager.desiredAttack == Attacks.SweepAttack)
                stateManager.SwitchState(stateManager.sweepAttackState);
        }

        if (Time.time >= changeState)
        {
            int attack = Random.Range(0, 1);
            if (attack == 0)
                stateManager.SwitchState(stateManager.mineAttackState);
        }
    }

    public override void HandleAnimations()
    {

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
