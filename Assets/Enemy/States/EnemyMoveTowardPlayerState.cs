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
            else if (stateManager.desiredAttack == Attacks.DoubleSlashAttack)
                stateManager.SwitchState(stateManager.doubleSlashAttackState);
            else if (stateManager.desiredAttack == Attacks.FlickAttack)
                stateManager.SwitchState(stateManager.flickAttackState);
            else if (stateManager.desiredAttack == Attacks.FlipAttack)
                stateManager.SwitchState(stateManager.flipAttackState);
            else if (stateManager.desiredAttack == Attacks.FlurryAttack)
                stateManager.SwitchState(stateManager.flurryAttackState);
            else if (stateManager.desiredAttack == Attacks.SweepAttack)
                stateManager.SwitchState(stateManager.sweepAttackState);
        }

        if (Time.time >= changeState)
        {
            int attack = Random.Range(0, 2);
            if (attack == 0)
                stateManager.SwitchState(stateManager.mineAttackState);
            else if (attack == 1)
                stateManager.SwitchState(stateManager.rangeAttackState);
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
