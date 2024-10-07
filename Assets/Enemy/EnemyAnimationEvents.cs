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
        stateManager.attackCooldownEnd = Time.time + Random.Range(stateManager.timeBetweenAttacks.x, stateManager.timeBetweenAttacks.y);
    }

    public void StartMove()
    {
        stateManager.meleeAttackState.move = true;
    }

    public void EndMove()
    {
        stateManager.meleeAttackState.move = false;
    }

    public void StopRotate()
    {
        stateManager.meleeAttackState.rotate = false;
    }

    public void EnableHitbox()
    {
        enemyHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        enemyHitbox.enabled = false;
    }

    public void SpawnProjectile()
    {
        stateManager.SpawnProjectile();
    }
}
