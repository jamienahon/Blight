using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyState currentState;

    public GameObject player;
    [HideInInspector] public EnemyHealthSystem healthSystem;
    [HideInInspector] public Animator animator;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyMoveState moveState = new EnemyMoveState();

    [HideInInspector] public bool switchStates = false;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Attacking")]
    public Vector2 timeBetweenAttacks;
    [HideInInspector] public float nextAttack;
    public float attackMoveSpeed;
    public float attackRange;
    public float damage;


    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        animator = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<EnemyHealthSystem>();
        nextAttack = Time.time + Random.Range(timeBetweenAttacks.x, timeBetweenAttacks.y);
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

    public void SwitchState(EnemyState state)
    {
        currentState = state;
        switchStates = true;
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) < attackRange;
    }

    public void OnTriggerEnter(Collider collider)
    {
        //if (collider.gameObject.tag == "Player")
        //{
        //    collider.GetComponentInParent<PlayerHealth>().DoDamage(1);
        //    collider.GetComponentInParent<PlayerStateManager>().SwitchState(collider.GetComponentInParent<PlayerStateManager>().getHitState);
        //}
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponentInParent<PlayerHealthSystem>().DoDamage(damage);
        }
    }

    public void OnTriggerExit(Collider collider)
    {

    }
}