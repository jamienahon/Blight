
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public PlayerStateManager stateManager;
    public Collider lHitbox;
    public Collider hHitbox;
    public GameObject healVFX;
    public GameObject BloodVFX;
    public AudioSource Hurt;
    public AudioSource HealSFX;
    public AudioSource Dodge;
    public AudioSource Step1;
    public AudioSource Step2;
    // public AudioSource DeathSFX;
    public GameObject shardVFX;
    public GameObject Glove;
    public Transform ShardParent;

    //Blood
    public ParticleSystem shard;
    public ParticleSystem BloodSplatter_P;
    public Transform BloodSpawn;

    public void StartDodge()
    {
        stateManager.isInvincible = true;
        Dodge.Play();
    }

    public void EndDodge()
    {
        stateManager.isInvincible = false;
        stateManager.dodgeState.check = false;

        if (stateManager.animator.GetFloat("WS") != 0 || stateManager.animator.GetFloat("AD") != 0)
        {
            stateManager.SwitchState(stateManager.altAttackState);
            return;
        }

        if (stateManager.animator.GetBool("IsMoving"))
            stateManager.SwitchState(stateManager.walkState);
        else if (stateManager.animator.GetBool("IsSprinting"))
            stateManager.SwitchState(stateManager.sprintState);
        else
            stateManager.SwitchState(stateManager.idleState);
    }

    public void EndAttack()
    {
        stateManager.animator.SetBool("IsHeavyAttack", false);
        if (!stateManager.animator.GetBool("IsCombo"))
        {
            stateManager.animator.SetBool("IsAttacking", false);
            stateManager.animator.SetBool("IsCombo", false);

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                stateManager.SwitchState(stateManager.idleState);
            else
                stateManager.SwitchState(stateManager.walkState);
        }
        stateManager.animator.SetBool("IsCombo", false);
    }

    public void EndAltAttack()
    {
        stateManager.animator.SetFloat("WS", 0);
        stateManager.animator.SetFloat("AD", 0);

        if (Input.GetAxis("Sprint") > 0)
            stateManager.SwitchState(stateManager.sprintState);
        else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
            stateManager.SwitchState(stateManager.walkState);
        else
            stateManager.SwitchState(stateManager.idleState);
    }

    public void EndCombo()
    {
        stateManager.shootState.combo = false;
        stateManager.animator.SetBool("IsCombo", false);
    }

    public void StartIdle()
    {
        stateManager.SwitchState(stateManager.idleState);
    }

    public void StartWalk()
    {
        stateManager.SwitchState(stateManager.walkState);
    }

    public void CheckCombo()
    {
        stateManager.shootState.combo = true;
    }

    public void PlayStep1()
    {
        Step1.Play();
    }
    public void PlayStep2()
    {
        Step2.Play();
    }

    public void StopStep1()
    {
        Step1.Stop();
    }
    public void StopStep2()
    {
        Step2.Stop();
    }

    public void StartMove()
    {
        stateManager.hAttackState.moveForward = true;
        stateManager.lAttackState.move = true;
        stateManager.dodgeState.move = true;
        stateManager.altAttackState.move = true;
    }

    public void StartMoveBack()
    {
        stateManager.heavyShootState.moveBack = true;
        stateManager.altAttackState.moveBack = true;
    }

    public void EndMoveBack()
    {
        stateManager.heavyShootState.moveBack = false;
        stateManager.altAttackState.moveBack = false;
    }

    public void StartMoveRight()
    {
        stateManager.altAttackState.moveRight = true;
    }

    public void EndMoveRight()
    {
        stateManager.altAttackState.moveRight = false;
    }

    public void StartMoveLeft()
    {
        stateManager.altAttackState.moveLeft = true;
    }

    public void EndMoveLeft()
    {
        stateManager.altAttackState.moveLeft = false;
    }

    public void DodgeCheck()
    {
        stateManager.dodgeState.check = true;
    }

    public void EndMove()
    {
        stateManager.hAttackState.moveForward = false;
        stateManager.lAttackState.move = false;
        stateManager.dodgeState.move = false;
        stateManager.altAttackState.move = false;
    }

    public void EndGetHit()
    {
        stateManager.animator.SetLayerWeight(1, 1);
        stateManager.animator.SetBool("IsHit", false);
        if (stateManager.animator.GetBool("IsMoving"))
            stateManager.SwitchState(stateManager.walkState);
        else
            stateManager.SwitchState(stateManager.idleState);
    }

    public void EnableHitboxL()
    {
        lHitbox.enabled = true;
    }

    public void EnableHitboxH()
    {
        hHitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        lHitbox.enabled = false;
        hHitbox.enabled = false;
    }

    public void EndParry()
    {
        stateManager.SwitchState(stateManager.idleState);
    }

    public void PauseBlock()
    {
        stateManager.animator.speed = 0;
    }
    public void PlayHurt()
    {
        Hurt.Play();
        Debug.Log("PLAYHURT");
    }

    //  public void PlayDeathSFX()
    //  {
    //       DeathSFX.Play();
    //       Debug.Log("PLAYDeath");
    //  }
    public void PlayHealSFX()
    {
        HealSFX.Play();
        shard.Play();
        Debug.Log("PLAYHealSFX");
    }

    public void SpawnHealVFX()
    {
        Instantiate(healVFX, stateManager.transform);

    }
    public void SpawnBloodVFX()
    {
        Instantiate(BloodVFX, BloodSpawn);
        Debug.Log("BloodSpawn");
        BloodSplatter_P.Play();
    }

    public void SpawnShardsVFX()
    {
        Instantiate(shardVFX, ShardParent);
    }

    public void Heal()
    {
        stateManager.healthSystem.Heal(stateManager.healAmount);
    }

    public void EndHeal()
    {
        stateManager.SwitchState(stateManager.idleState);
    }

    public void SpawnProjectile()
    {
        stateManager.healthSystem.ConsumeStamina(stateManager.lightAttackStamCost);
        stateManager.playerAudio.clip = stateManager.shootState.shootSound;
        stateManager.playerAudio.loop = stateManager.shootState.loopSound;
        stateManager.playerAudio.Play();

        Vector3 position = new Vector3(stateManager.transform.position.x + 0.15f, stateManager.transform.position.y + 1.5f, stateManager.transform.position.z);
        GameObject newProjectile = Instantiate(stateManager.projectile, position, stateManager.projectile.transform.rotation);

        GameObject target = GameObject.Find("LockOn Point R");

        if (stateManager.isLockedOn)
        {
            newProjectile.transform.up = (target.transform.position - newProjectile.transform.position).normalized;
            newProjectile.GetComponent<PlayerProjectile>().InitialiseArrowValues(stateManager.gameObject, target, stateManager.lightAttackDamage, stateManager.lightAttackGemRecharge, stateManager.lockedOnArrowTrackingStrength, stateManager.arrowLifetime);
        }
        else
        {
            newProjectile.transform.up = stateManager.animator.transform.forward;
            newProjectile.GetComponent<PlayerProjectile>().InitialiseArrowValues(stateManager.gameObject, target, stateManager.lightAttackDamage, stateManager.lightAttackGemRecharge, stateManager.arrowTrackingStrength, stateManager.arrowLifetime);
        }

    }

    public void SpawnMultiProjectile()
    {
        stateManager.healthSystem.ConsumeStamina(stateManager.heavyAttackStamCost);
        stateManager.playerAudio.clip = stateManager.heavyShootState.heavyShootSound;
        stateManager.playerAudio.loop = stateManager.heavyShootState.loopSound;
        stateManager.playerAudio.Play();

        for (int rotation = -45; rotation <= 45; rotation += 45)
        {
            Vector3 position = new Vector3(stateManager.transform.position.x + 0.15f, stateManager.transform.position.y + 1.5f, stateManager.transform.position.z);
            GameObject newProjectile = Instantiate(stateManager.heavyProjectile, position, stateManager.heavyProjectile.transform.rotation);

            GameObject target = GameObject.Find("LockOn Point R");

            if (stateManager.isLockedOn)
            {
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * (target.transform.position - newProjectile.transform.position).normalized;
                newProjectile.GetComponent<PlayerProjectile>().InitialiseArrowValues(stateManager.gameObject, target, stateManager.heavyAttackDamage / 3.0f, stateManager.lightAttackGemRecharge, stateManager.lockedOnArrowTrackingStrength, stateManager.arrowLifetime);
            }
            else
            {
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * stateManager.animator.transform.forward;
                newProjectile.GetComponent<PlayerProjectile>().InitialiseArrowValues(stateManager.gameObject, target, stateManager.heavyAttackDamage / 3.0f, stateManager.lightAttackGemRecharge, stateManager.arrowTrackingStrength, stateManager.arrowLifetime);
            }

        }

    }
}
