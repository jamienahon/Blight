using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Animator animator;
    public float timeToExplosion;
    public float damage;
    public Collider mineCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 1 / timeToExplosion;
    }

    public void Explode()
    {
        animator.speed = 1;
        mineCollider.enabled = true;
    }

    public void DestroyMine()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStateManager playerStateManager = other.gameObject.GetComponent<PlayerStateManager>();
        if (other.gameObject.tag == "Player" && !playerStateManager.isInvincible)
        {
            other.GetComponent<PlayerHealthSystem>().DoDamage(damage);

            playerStateManager.SwitchState(playerStateManager.getHitState);
        }
    }
}
