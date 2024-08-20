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

        stateManager.enemySound.clip = null;
    }

    public override void UpdateState()
    {
        if (stateManager.IsPlayerInRange())
        {

            if (Time.time >= stateManager.nextAttack)
            {
                stateManager.SwitchState(stateManager.attackState);
                stateManager.nextAttack = Time.time + Random.Range(stateManager.timeBetweenAttacks.x, stateManager.timeBetweenAttacks.y);
            }
        }
        else
        {
            stateManager.SwitchState(stateManager.moveState);
        }

        Vector3 lookDir = new Vector3(stateManager.player.transform.position.x, 0, stateManager.player.transform.position.z);
        stateManager.animator.transform.LookAt(lookDir);
        stateManager.animator.transform.rotation = Quaternion.Euler(0, stateManager.animator.transform.rotation.eulerAngles.y, 0);
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsMoving", false);
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}
