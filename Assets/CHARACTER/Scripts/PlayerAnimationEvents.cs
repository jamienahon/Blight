
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

    public void StartDodge()
    {
        stateManager.isInvincible = true;
        Dodge.Play();
    }

    public void EndDodge()
    {
        
        stateManager.animator.SetLayerWeight(1, 1);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.isInvincible = false;
        if (stateManager.animator.GetBool("IsMoving"))
            stateManager.SwitchState(stateManager.walkState);
        else
            stateManager.SwitchState(stateManager.idleState);
    }

    public void EndAttack()
    {
        stateManager.animator.SetLayerWeight(1, 1);
        stateManager.animator.SetBool("IsLightAttacking", false);
        stateManager.animator.SetBool("IsHeavyAttacking", false);
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            stateManager.SwitchState(stateManager.idleState);
        else
            stateManager.SwitchState(stateManager.walkState);

        if (Input.GetAxis("LAttack") > 0)
        {
            stateManager.SwitchState(stateManager.shootState);
            stateManager.animator.SetBool("IsLightAttacking", true);
        }
    }

    public void PlayStep1()
    {
        Step1.Play();
    }
    public void PlayStep2()
    {
        Step2.Play();
    }

    public void StartMove()
    {
        stateManager.hAttackState.moveForward = true;
        stateManager.lAttackState.move = true;
        stateManager.dodgeState.move = true;
    }

    public void StartMoveBack()
    {
        stateManager.heavyShootState.moveBack = true;
    }

    public void EndMoveBack()
    {
        stateManager.heavyShootState.moveBack = false;
    }

    public void EndMove()
    {
        stateManager.hAttackState.moveForward = false;
        stateManager.lAttackState.move = false;
        stateManager.dodgeState.move = false;
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
        Debug.Log("PLAYHealSFX");
    }

    public void SpawnHealVFX()
    {
        Instantiate(healVFX, stateManager.transform);
    }
    public void SpawnBloodVFX()
    {
        Instantiate(BloodVFX, stateManager.transform);
        Debug.Log("BloodSpawn");
    }

    public void SpawnShardsVFX()
    {
        Instantiate(shardVFX, stateManager.animator.transform.position, stateManager.animator.transform.rotation);
    }

    public void Heal()
    {
        stateManager.healthSystem.Heal(stateManager.healAmount);
    }

    public void EndHeal()
    {
        stateManager.animator.SetBool("IsHealing", false);

        if (stateManager.animator.GetBool("IsMoving"))
            stateManager.SwitchState(stateManager.walkState);
        else
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
