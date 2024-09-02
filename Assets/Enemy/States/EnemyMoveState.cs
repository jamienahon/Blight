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
        HandleAuido();
    }

    public override void UpdateState()
    {
        Vector3 lookDir = new Vector3(stateManager.player.transform.position.x, 0, stateManager.player.transform.position.z);
        stateManager.animator.transform.LookAt(lookDir);
        stateManager.animator.transform.rotation = Quaternion.Euler(0, stateManager.animator.transform.rotation.eulerAngles.y, 0);

        stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.moveSpeed * Time.deltaTime);

        if (Time.time >= stateManager.nextAttack)
        {
            stateManager.SwitchState(stateManager.idleState);
        }
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsMoving", true);
    }

    public override void HandleAuido()
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
