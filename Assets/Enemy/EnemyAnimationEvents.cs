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

    //Death State variables
    public Animation victoryDoor;
    public CameraController camCont;
    public GameObject victoryScreen;

    //sounds
    public AudioSource sweep;
    public AudioSource SwipingSFX;
    public AudioSource RoarSFX;
    public AudioSource Deathsfx;
    public AudioSource Flick;
    public AudioSource Flurry;
    public AudioSource Flip;





    bool moveTowardPlayer = false;

    private void Update()
    {
        if (moveTowardPlayer)
        {
            if (Vector3.Distance(stateManager.transform.position, stateManager.player.transform.position) > stateManager.maxAttackDistance)
                stateManager.transform.position += transform.forward * stateManager.midAttackMoveSpeed * Time.deltaTime;
        }
    }

    //Play audios
    public void PlaySwiping()
    {
        SwipingSFX.Play();
    }
    public void PlayRoar()
    {
        RoarSFX.Play();

    }
    public void PlaySweep()
    {
        sweep.Play();
    }

    public void PlayDeath()
    {
        Deathsfx.Play();
    }
    public void PlayFlip()
    {
        Flip.Play();
    }
    public void PlayFlick()
    {
        Flick.Play();    
    }
    public void PlayFlurry()
    {
        Flurry.Play();
    }

    //Stop audios
    public void StopSwiping()
    {
        SwipingSFX.Stop();
    }
    public void StopRoar()
    {
        RoarSFX.Stop();

    }
    public void StopSweep()
    {
        sweep.Stop();
    }

    public void StopDeath()
    {
        Deathsfx.Stop();
    }
    public void StopFlip()
    {
        Flip.Stop();
    }
    public void StopFlick()
    {
        Flick.Stop();
    }
    public void StopFlurry()
    {
        Flurry.Stop();
    }


    public void EndAttack()
    {
        stateManager.SwitchState(stateManager.idleState);
        stateManager.attackCooldownEnd = Time.time + Random.Range(stateManager.timeBetweenAttacks.x, stateManager.timeBetweenAttacks.y);
    }

    public void StartRotate()
    {
        stateManager.facePlayer = true;
    }

    public void StopRotate()
    {
        stateManager.facePlayer = false;
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

    public void StartMoveTowardPlayer(float distance)
    {
        moveTowardPlayer = true;
        stateManager.maxAttackDistance = distance;
    }

    public void EndMoveTowardPlayer()
    {
        moveTowardPlayer = false;
    }

    public void Death()
    {

        camCont.EndLockOn();
        victoryDoor.Play();
        victoryScreen.SetActive(true);

    }

}
