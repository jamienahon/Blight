using System;
using UnityEngine;

public enum Attacks
{
    SweepAttack,
    MineAttack,
    SlashAttack,
    RangedAttack
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
    public EnemyDeathState EnemyDeathState = new EnemyDeathState();


    [HideInInspector] public bool switchStates = false;

    [Header("Movement")]
    public float moveSpeed;
    public float fastMoveSpeed;
    public float tooClose;
    public float tooFar;
    public Vector2 changeMovementRange;
    [HideInInspector] public float changeMovement;
    public float timeToGetInRange;

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

    [HideInInspector] public Attacks previousAttack;
    [HideInInspector] public Attacks desiredAttack;
    int currentMoveDir = 0;


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
        DecideAttack();

        Vector3 lookDir = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        animator.transform.LookAt(lookDir);
        animator.transform.rotation = Quaternion.Euler(0, animator.transform.rotation.eulerAngles.y, 0);

        if (currentState == idleState)
        {
            DecideMovementDirection();
        }
    }

    private void DecideMovementDirection()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > tooFar)
            transform.Translate(animator.transform.forward * moveSpeed * Time.deltaTime);
        else if (Vector3.Distance(player.transform.position, transform.position) < tooClose)
            transform.Translate(-animator.transform.forward * moveSpeed * Time.deltaTime);
        else if (Vector3.Distance(player.transform.position, transform.position) <= tooFar)
        {
            if (currentMoveDir == 0)
                transform.Translate(-animator.transform.right * moveSpeed * Time.deltaTime);
            else if (currentMoveDir == 1)
                transform.Translate(animator.transform.right * moveSpeed * Time.deltaTime);
        }

        if (Time.time >= changeMovement)
        {
            currentMoveDir = UnityEngine.Random.Range(0, 3);
            changeMovement = Time.time + UnityEngine.Random.Range(changeMovementRange.x, changeMovementRange.y);
        }
    }

    public void SwitchState(EnemyState state)
    {
        currentState = state;
        switchStates = true;
    }

    public void DecideAttack()
    {
        if (Time.time >= attackCooldownEnd && (currentState == idleState || currentState == moveState))
        {
            int attackType;
            do attackType = UnityEngine.Random.Range(0, Enum.GetNames(typeof(Attacks)).Length);
            while (attackType == (int)previousAttack);

            if (attackType == (int)Attacks.SweepAttack)
            {
                if (!IsPlayerInRange())
                {
                    desiredAttack = Attacks.SweepAttack;
                    SwitchState(moveTowardPlayerState);
                }
                else
                {
                    SwitchState(sweepAttackState);
                }
            }
            else if (attackType == (int)Attacks.MineAttack)
            {
                SwitchState(mineAttackState);
            }
            else if (attackType == (int)Attacks.SlashAttack)
            {
                if (!IsPlayerInRange())
                {
                    desiredAttack = Attacks.SlashAttack;
                    SwitchState(moveTowardPlayerState);
                }
                else
                {
                    SwitchState(slashAttackState);
                }
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
