using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public EnemyStateManager stateManager;
    public CapsuleCollider enemyHitbox;

    public void EndAttack()
    {
        stateManager.SwitchState(stateManager.idleState);
    }

    public void StartMove()
    {
        stateManager.attackState.move = true;
    }

    public void EndMove()
    {
        stateManager.attackState.move = false;
    }

    public void StopRotate()
    {
        stateManager.attackState.rotate = false;
    }

    public void EnableHitbox()
    {
        enemyHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        enemyHitbox.enabled = false;
    }
}
