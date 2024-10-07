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
    }

    public override void UpdateState()
    {
        //if (stateManager.IsPlayerInRange() && Time.time >= stateManager.nextAttack)
        //{
        //    if (!stateManager.isInSecondPhase)
        //    {
        //        stateManager.SwitchState(stateManager.meleeAttackState);
        //    }
        //    else
        //    {
        //        int randomNum = Random.Range(0, 1);
        //        if(randomNum == 0)
        //            stateManager.SwitchState(stateManager.rangeAttackState);
        //        else
        //            stateManager.SwitchState(stateManager.meleeAttackState);
        //    }
        //    stateManager.nextAttack = Time.time + Random.Range(stateManager.timeBetweenAttacks.x, stateManager.timeBetweenAttacks.y);
        //}
        //else
        //{
        //    stateManager.SwitchState(stateManager.moveState);
        //}

        if (Time.time >= stateManager.attackCooldownEnd)
        {
            if (!stateManager.isInSecondPhase)
            {
                if (stateManager.IsPlayerInRange())
                    stateManager.SwitchState(stateManager.mineAttackState);
                else
                    stateManager.SwitchState(stateManager.moveState);
            }
            else
            {
                if (Vector3.Distance(stateManager.gameObject.transform.position, stateManager.player.gameObject.transform.position) < stateManager.attackRange)
                {
                    stateManager.SwitchState(stateManager.meleeAttackState);
                }
                else if (Vector3.Distance(stateManager.gameObject.transform.position, stateManager.player.gameObject.transform.position) < stateManager.rangedAttackRange)
                {
                    stateManager.SwitchState(stateManager.rangeAttackState);
                }
                else
                    stateManager.SwitchState(stateManager.moveState);
            }
            stateManager.attackCooldownEnd = Time.time + Random.Range(stateManager.timeBetweenAttacks.x, stateManager.timeBetweenAttacks.y);
        }
        else
            stateManager.SwitchState(stateManager.idleState);

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
