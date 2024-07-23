using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public BoxCollider boxCollider;

    void Start()
    {
        //boxCollider = GetComponentInChildren<BoxCollider>();
    }

    public void StartAttack()
    {
        boxCollider.enabled = true;
    }

    public void EndAttack()
    {
        boxCollider.enabled = false;
    }
}
