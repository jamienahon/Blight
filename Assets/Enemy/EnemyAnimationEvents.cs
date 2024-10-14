using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hitboxes
{
    SpinAttackHitbox,
    RightArmHitboxes,
    LeftArmHitboxes
}

public class EnemyAnimationEvents : MonoBehaviour
{
    public EnemyStateManager stateManager;
    public Collider[] spinAttackHitboxes;
    public Collider[] rightArmHitboxes;
    public Collider[] leftArmHitboxes;

    public void EndAttack()
    {
        stateManager.SwitchState(stateManager.idleState);
        stateManager.attackCooldownEnd = Time.time + Random.Range(stateManager.timeBetweenAttacks.x, stateManager.timeBetweenAttacks.y);
    }

    public void StopRotate()
    {
        stateManager.meleeAttackState.rotate = false;
    }

    public void EnableHitbox(Hitboxes hitbox)
    {
        if (hitbox == Hitboxes.SpinAttackHitbox)
        {
            foreach (Collider collider in spinAttackHitboxes)
            {
                collider.enabled = true;
            }
        }
        else if (hitbox == Hitboxes.RightArmHitboxes)
        {
            foreach (Collider collider in rightArmHitboxes)
            {
                collider.enabled = true;
            }
        }
        else if (hitbox == Hitboxes.LeftArmHitboxes)
        {
            foreach (Collider collider in leftArmHitboxes)
            {
                collider.enabled = true;
            }
        }
    }

    public void DisableHitbox(Hitboxes hitbox)
    {
        if (hitbox == Hitboxes.SpinAttackHitbox)
        {
            foreach (Collider collider in spinAttackHitboxes)
            {
                collider.enabled = false;
            }
        }
        else if (hitbox == Hitboxes.RightArmHitboxes)
        {
            foreach (Collider collider in rightArmHitboxes)
            {
                collider.enabled = false;
            }
        }
        else if (hitbox == Hitboxes.LeftArmHitboxes)
        {
            foreach (Collider collider in leftArmHitboxes)
            {
                collider.enabled = false;
            }
        }
    }

    public void SpawnProjectile()
    {
        stateManager.SpawnProjectile();
    }
}
