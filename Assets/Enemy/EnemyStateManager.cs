using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public enum Attacks
{
    SweepAttack,
    MineAttack,
    SlashAttack
}

public class EnemyStateManager : MonoBehaviour
{
    public EnemyState currentState;

    public GameObject player;
    [HideInInspector] public EnemyHealthSystem healthSystem;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource enemyAudio;
    [HideInInspector] public SkinnedMeshRenderer meshRenderer;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyAttackState meleeAttackState = new EnemyAttackState();
    public EnemyRangeAttackState rangeAttackState = new EnemyRangeAttackState();
    public EnemyMoveState moveState = new EnemyMoveState();
    public EnemyStunnedState stunnedState = new EnemyStunnedState();
    public EnemyMineAttackState mineAttackState = new EnemyMineAttackState();
    public EnemyMoveTowardPlayerState moveTowardPlayerState = new EnemyMoveTowardPlayerState();
    public EnemySpinAttackState sweepAttackState = new EnemySpinAttackState();
    public EnemySlashAttackState slashAttackState = new EnemySlashAttackState();

    [HideInInspector] public bool switchStates = false;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Attacking")]
    public Vector2 timeBetweenAttacks;
    [HideInInspector] public float attackCooldownEnd;
    public float attackMoveSpeed;
    public float attackRange;
    public float rangedAttackRange;
    public float meleeDamage;
    public float rangedDamage;
    public GameObject projectile;
    public float arrowTrackingStrength;
    public float arrowMoveSpeed;
    public float midAttackMoveSpeed;
    public float maxAttackDistance;

    [Header("Mine Attack")]
    public GameObject minePrefab;
    public float mineDamage;
    public float mineSpawnRange;
    public float numberOfMines;
    public float timeToExplosion;
    public float mineAttackLength;

    [Header("Phases")]
    public Color phase1Colour;
    public Color phase2Colour;
    [HideInInspector] public bool isInSecondPhase = false;

    public float stunnedLength;
    [HideInInspector] public float endStun;

    Attacks previousAttack;
    Attacks desiredAttack;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<EnemyHealthSystem>();
        enemyAudio = GetComponent<AudioSource>();
        attackCooldownEnd = Time.time + UnityEngine.Random.Range(timeBetweenAttacks.x, timeBetweenAttacks.y);
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshRenderer.material.color = phase1Colour;

        currentState = idleState;
        currentState.EnterState(this);

        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
    }

    public void FixedUpdate()
    {
        if (switchStates)
        {
            currentState.EnterState(this);
            switchStates = false;
        }

        currentState.UpdateState();
        DecideState();

        Vector3 lookDir = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        animator.transform.LookAt(lookDir);
        animator.transform.rotation = Quaternion.Euler(0, animator.transform.rotation.eulerAngles.y, 0);
    }

    public void SwitchState(EnemyState state)
    {
        currentState = state;
        switchStates = true;
    }

    public void DecideState()
    {
        if (Time.time >= attackCooldownEnd && (currentState == idleState || currentState == moveState))
        {
            int attackType;
            do attackType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Attacks)).Length);
            while (attackType == (int)previousAttack);

            if (attackType == (int)Attacks.SweepAttack)
            {
                SwitchState(sweepAttackState);
                previousAttack = Attacks.SweepAttack;
            }
            else if (attackType == (int)Attacks.MineAttack)
            {
                SwitchState(mineAttackState);
                previousAttack = Attacks.MineAttack;
            }
            else if (attackType == (int)Attacks.SlashAttack)
            {
                SwitchState(slashAttackState);
                previousAttack = Attacks.SlashAttack;
            }
        }
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) < attackRange;
    }

    public void SpawnProjectile()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 2.25f, transform.position.z);
        GameObject newProjectile = Instantiate(projectile, position, projectile.transform.rotation);
        EnemyProjectile arrowScript = newProjectile.GetComponent<EnemyProjectile>();
        arrowScript.enemy = gameObject;
        arrowScript.trackingStrength = arrowTrackingStrength;
        arrowScript.moveSpeed = arrowMoveSpeed;
        arrowScript.damage = rangedDamage;

        arrowScript.target = player.gameObject;
        Vector3 targetPos = new Vector3(arrowScript.target.transform.position.x, arrowScript.target.transform.position.y + 2, arrowScript.target.transform.position.z);

        newProjectile.transform.up = (targetPos - newProjectile.transform.position).normalized;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponentInParent<PlayerHealthSystem>().DoDamage(meleeDamage);
        }
    }

    public void OnTriggerExit(Collider collider)
    {

    }
}
