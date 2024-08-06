using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }
    public bool move;
    public bool rotate;

    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        move = false;
        rotate = true;
        HandleAnimations();
        SetAnimationParameters();
    }

    public override void UpdateState()
    {
        if (move)
            stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.attackMoveSpeed * Time.deltaTime);

        if (rotate)
        {
            Vector3 lookDir = new Vector3(stateManager.player.transform.position.x, 0, stateManager.player.transform.position.z);
            stateManager.animator.transform.LookAt(lookDir);
            stateManager.animator.transform.rotation = Quaternion.Euler(0, stateManager.animator.transform.rotation.eulerAngles.y, 0);
        }
    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Unarmed-Attack-R2");
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
