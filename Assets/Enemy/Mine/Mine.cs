using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Animator animator;
    public float timeToExplosion;
    public Collider mineCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed =  1 / timeToExplosion;
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
}
