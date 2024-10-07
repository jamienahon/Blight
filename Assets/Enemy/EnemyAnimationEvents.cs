using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hitboxes
{
    SpinAttackHitbox,
    SlashAttackHitboxes
}

public class EnemyAnimationEvents : MonoBehaviour
{
    public EnemyStateManager stateManager;
    public Collider spinAttackHitbox;
    public Collider[] slashAttackHitboxes;

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

    public void EnableHitbox(Hitboxes hitbox)
    {
        if(hitbox == Hitboxes.SpinAttackHitbox)
        {
            spinAttackHitbox.enabled = true;
        }
    }

    public void DisableHitbox(Hitboxes hitbox)
    {
        if (hitbox == Hitboxes.SpinAttackHitbox)
        {
            spinAttackHitbox.enabled = false;
        }
    }

    public void SpawnProjectile()
    {
        stateManager.SpawnProjectile();
    }
}
