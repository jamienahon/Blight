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

    bool moveTowardPlayer = false;

    private void Update()
    {
        if (moveTowardPlayer)
        {
            Vector3 enemyPos = new Vector3(stateManager.transform.position.x, 0, stateManager.transform.position.z);
            Vector3 playerPos = new Vector3(stateManager.player.transform.position.x, 0, stateManager.player.transform.position.z);

            if (Vector3.Distance(enemyPos, playerPos) > stateManager.maxAttackDistance)
            {
                float yPos = stateManager.transform.position.y;
                Vector3 newPos = Vector3.MoveTowards(stateManager.transform.position, stateManager.player.transform.position, stateManager.midAttackMoveSpeed * Time.deltaTime);
                stateManager.transform.position = new Vector3(newPos.x, yPos, newPos.z);
            }
        }
    }

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

    public void SpawnMines()
    {
        stateManager.mineAttackState.SpawnMines();
    }

    public void StartMoveTowardPlayer()
    {
        moveTowardPlayer = true;
    }

    public void EndMoveTowardPlayer()
    {
        moveTowardPlayer = false;
    }
}
