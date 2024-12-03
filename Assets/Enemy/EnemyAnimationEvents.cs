using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioSource DBLSwipingSFX;

    //particle cue
    public ParticleSystem DustWave_P;
    public Transform DustWave;


    //UI
    public GameObject BossBar;


    GameObject proj;

    CinemachineBasicMultiChannelPerlin cameraNoise;

    bool moveTowardPlayer = false;

    private void Start()
    {
        cameraNoise = camCont.lockOnCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

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
    public void PlayDBLSwiping()
    {
        DBLSwipingSFX.Play();
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
    public void StopDBLSwiping()
    {
        DBLSwipingSFX.Stop();
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

    public void SpawnStillProjectile()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 10.75f, transform.position.z - 3.0f);
        proj = Instantiate(stateManager.projectile, position, stateManager.projectile.transform.rotation);
        EnemyProjectile arrowScript = proj.GetComponent<EnemyProjectile>();
        arrowScript.enemy = gameObject;
        arrowScript.trackingStrength = stateManager.arrowTrackingStrength;
        arrowScript.moveSpeed = 0;
        arrowScript.damage = stateManager.rangedDamage;

        arrowScript.target = stateManager.player.gameObject;
        Vector3 targetPos = new Vector3(arrowScript.target.transform.position.x, arrowScript.target.transform.position.y + 2, arrowScript.target.transform.position.z);

        proj.transform.up = (targetPos - proj.transform.position).normalized;
    }

    public void FireStillProjectile()
    {
        if(proj)
        {
            proj.GetComponent<EnemyProjectile>().moveSpeed = stateManager.arrowMoveSpeed;
        }
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
        BossBar.SetActive (false);

    }

    public void StartCameraShake()
    {
        cameraNoise.m_AmplitudeGain = 3f;
    }

    public void EndCameraShake()
    {
        cameraNoise.m_AmplitudeGain = 0;
    }

    public void SpawnDust()
    {
        DustWave_P.Play();
    }
}
