using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

public class Mine : MonoBehaviour
{
    Animator animator;
    public float timeToExplosion;
    public float damage;
    Collider mineCollider;
    VisualEffect explosionVFX;

    private void Start()
    {
        animator = GetComponent<Animator>();
        mineCollider = GetComponent<Collider>();
        animator.speed = 1 / timeToExplosion;
        explosionVFX = GetComponent<VisualEffect>();
    }

    public void Explode()
    {
        animator.speed = 1;
        mineCollider.enabled = true;
        explosionVFX.Play();
    }

    public void DisableHitbox()
    {
        mineCollider.enabled = false;
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
