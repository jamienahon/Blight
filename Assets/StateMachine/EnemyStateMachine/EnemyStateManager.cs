using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyState currentState;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyMoveState moveState = new EnemyMoveState();

    public Animator animator;
    [HideInInspector] public bool switchStates = false;

    public Vector2 timeBetweenAttacks;
    [HideInInspector] public float nextAttack;
    public float attackMoveAmount;
    public GameObject player;
    public float attackRange;
    public float moveSpeed;

    private void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
        //animator = GetComponentInChildren<Animator>();
        nextAttack = Time.time + Random.Range(timeBetweenAttacks.x, timeBetweenAttacks.y);
    }

    public void Update()
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

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(collision);
    }
}
