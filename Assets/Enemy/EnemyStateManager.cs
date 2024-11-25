using System;
using UnityEngine;

public enum Attacks
{
    SweepAttack,
    SlashAttack,
    DoubleSlashAttack,
    FlickAttack,
    FlipAttack,
    FlurryAttack,
    MineAttack,
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
    public EnemyFlickAttackState flickAttackState = new EnemyFlickAttackState();
    public EnemyFlurryAttackState flurryAttackState = new EnemyFlurryAttackState();
    public EnemyFlipAttackState flipAttackState = new EnemyFlipAttackState();
    public EnemyDoubleSlashAttackState doubleSlashAttackState = new EnemyDoubleSlashAttackState();

    [HideInInspector] public bool switchStates = false;

    [Header("Movement")]
    public float moveSpeed;
    public float fastMoveSpeed;
    public float tooClose;
    public float tooFar;
    public Vector2 changeMovementRange;
    [HideInInspector] public float changeMovement;
    public float timeToGetInRange;
    public bool facePlayer = true;
    public float rotationSpeed;
    public GameObject arenaCentre;
    public float maxDistanceFromCentre;

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

        if (facePlayer)
        {
            Quaternion lookRotation = Quaternion.LookRotation((player.transform.position - transform.position).normalized);
            lookRotation.x = lookRotation.z = 0;
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        if (currentState == idleState)
        {
            DecideMovementDirection();
        }
    }

    private void DecideMovementDirection()
    {
        if (Vector3.Distance(transform.position, arenaCentre.transform.position) > maxDistanceFromCentre)
        {
            transform.Translate((arenaCentre.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime);
            return;
        }

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
            do desiredAttack = (Attacks)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Attacks)).Length);
            while (desiredAttack == previousAttack);

            desiredAttack = Attacks.SlashAttack;

            if (desiredAttack == Attacks.MineAttack)
            {
                SwitchState(mineAttackState);
            }
            else if (desiredAttack == Attacks.RangedAttack)
            {
                SwitchState(rangeAttackState);
            }
            else
            {
                if (!IsPlayerInRange())
                {
                    SwitchState(moveTowardPlayerState);
                }
                else
                {
                    if (desiredAttack == Attacks.SlashAttack)
                        SwitchState(slashAttackState);
                    else if (desiredAttack == Attacks.DoubleSlashAttack)
                        SwitchState(doubleSlashAttackState);
                    else if (desiredAttack == Attacks.FlickAttack)
                        SwitchState(flickAttackState);
                    else if (desiredAttack == Attacks.FlipAttack)
                        SwitchState(flipAttackState);
                    else if (desiredAttack == Attacks.FlurryAttack)
                        SwitchState(flurryAttackState);
                    else if (desiredAttack == Attacks.SweepAttack)
                        SwitchState(sweepAttackState);
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
