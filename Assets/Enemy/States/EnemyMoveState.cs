using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }

    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
    }

    public override void UpdateState()
    {
        Vector3 lookDir = new Vector3(stateManager.player.transform.position.x, 0, stateManager.player.transform.position.z);
        stateManager.animator.transform.LookAt(lookDir);

        stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.moveSpeed * Time.deltaTime);

        if (stateManager.IsPlayerInRange())
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsMoving", true);
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}