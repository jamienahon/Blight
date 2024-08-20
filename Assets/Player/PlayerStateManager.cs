using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.FilePathAttribute;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState;

    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerHealthSystem healthSystem;
    [HideInInspector]public AudioSource playerAudio;

    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    public PlayerSprintState sprintState = new PlayerSprintState();
    public PlayerDodgeState dodgeState = new PlayerDodgeState();
    public PlayerJumpState jumpState = new PlayerJumpState();
    public PlayerLAttackState lAttackState = new PlayerLAttackState();
    public PlayerHAttackState hAttackState = new PlayerHAttackState();
    public PlayerGetHitState getHitState = new PlayerGetHitState();
    public PlayerBlockState blockState = new PlayerBlockState();
    public PlayerShootState shootState = new PlayerShootState();
    public PlayerHeavyShootState heavyShootState = new PlayerHeavyShootState();
    public PlayerHealState healState = new PlayerHealState();

    [HideInInspector] public bool isLockedOn = false;
    [HideInInspector] public bool switchStates = false;
    [HideInInspector] public bool isInvincible = false;

    [HideInInspector] public Vector3 movementDirection = new Vector3();

    [Header("Movement")]
    public float runSpeed;
    public float sprintSpeed;
    public float sprintStamCost;
    public float healingMoveSpeed;

    [Header("Dodging")]
    public float dodgeMoveSpeed;
    public float dodgeStamCost;

    [Header("Jumping")]
    public float jumpHeight;
    public float jumpSpeedWalk;
    public float jumpSpeedSprint;
    public float jumpStamCost;

    [Header("Attacking")]
    public float arrowTrackingStrength;
    public float attackMoveSpeed;

    [Header("Light Attack")]
    public GameObject projectile;
    public float lightAttackDamage;
    public float lightAttackStamCost;
    public float lightAttackGemRecharge;

    [Header("Heavy Attack")]
    public float heavyAttackDamage;
    public float heavyAttackStamCost;
    public float heavyAttackGemRecharge;

    [Header("Blocking")]
    public float blockDamageReduction;
    public float blockStamCost;
    public float blockMoveSpeed;
    public float blockPauseTime;
    [HideInInspector] public float endBlockPause;

    [Header("Healing")]
    public float healLengthSeconds;
    public float healAmount;

    private void Start()
    {
        isLockedOn = false;
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<PlayerHealthSystem>();
        playerAudio = GetComponent<AudioSource>();

        currentState = idleState;
        currentState.EnterState(this);
    }

    public void FixedUpdate()
    {
        if (switchStates)
        {
            currentState.EnterState(this);
            switchStates = false;
        }
        currentState.UpdateState();
    }

    public void SwitchState(PlayerState state)
    {
        currentState = state;
        switchStates = true;
    }

    public void SpawnProjectile()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 2.25f, transform.position.z);
        GameObject newProjectile = Instantiate(projectile, position, projectile.transform.rotation);
        MoveProjectile arrowScript = newProjectile.GetComponent<MoveProjectile>();
        arrowScript.player = gameObject;
        arrowScript.trackingStrength = arrowTrackingStrength;


        if (isLockedOn)
        {
            arrowScript.target = Camera.main.gameObject.GetComponent<CameraController>().currentLockOnPoint.gameObject;
            Vector3 lockOnPos = Camera.main.gameObject.GetComponent<CameraController>().currentLockOnPoint.gameObject.transform.position;

            newProjectile.transform.up = (lockOnPos - newProjectile.transform.position).normalized;
        }
        else
        {
            newProjectile.transform.up = animator.transform.forward;
        }
    }

    public void SpawnMultiProjectile()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 2.25f, transform.position.z);
        if (isLockedOn)
        {
            for (int rotation = -45; rotation <= 45; rotation += 45)
            {
                GameObject newProjectile = Instantiate(projectile, position, projectile.transform.rotation);
                MoveProjectile arrowScript = newProjectile.GetComponent<MoveProjectile>();
                arrowScript.target = Camera.main.gameObject.GetComponent<CameraController>().currentLockOnPoint.gameObject;
                arrowScript.player = gameObject;
                arrowScript.trackingStrength = arrowTrackingStrength;

                Vector3 lockOnPos = Camera.main.gameObject.GetComponent<CameraController>().currentLockOnPoint.gameObject.transform.position;
                newProjectile.transform.up = (lockOnPos - newProjectile.transform.position).normalized;
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * newProjectile.transform.up;
            }
        }
        else
        {
            for (int rotation = -45; rotation <= 45; rotation += 45)
            {
                GameObject newProjectile = Instantiate(projectile, position, projectile.transform.rotation);

                newProjectile.transform.up = animator.transform.forward;
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * newProjectile.transform.up;
            }
        }
    }


    public void OnTriggerEnter(Collider collider)
    {
        currentState.OnCollisionEnter(collider);
    }

    public void OnTriggerExit(Collider collider)
    {

    }
}
