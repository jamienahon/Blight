using Cinemachine;
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
    public void PlaySwiping()
    {
        SwipingSFX.Play();
        Debug.Log("PLAYswipe");
    }
    public void PlayRoar()
    {
        RoarSFX.Play();
        Debug.Log("PLAYroar");
    }
    public void PlaySweep()
    {
        sweep.Play();
        Debug.Log("PLAYsweep");
    }

    public void PlayDeath()
    {
        Deathsfx.Play();
        Debug.Log("PLAYDeath");
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

    }

    public void StartCameraShake()
    {
        cameraNoise.m_AmplitudeGain = 10;
    }

    public void EndCameraShake()
    {
        cameraNoise.m_AmplitudeGain = 0;
    }

}
