using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    PlayerStateManager stateManager;

    public float damage;
    public float gemRechargeAmount;
    public float moveSpeed;
    public float trackingStrength;
    public float damageFalloff;

    private void Update()
    {
        if (target)
        {
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) <
                Vector3.Distance(player.transform.position, target.transform.position) &&
                Vector3.Distance(gameObject.transform.position, target.transform.position) < stateManager.arrowTrackingRange)
            {
                Vector3 delta = (target.transform.position - transform.position).normalized;
                transform.up = Vector3.RotateTowards(transform.up, delta, trackingStrength * Time.deltaTime, 0.0f);
            }
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;

        if (damage > 0)
            damage -= damageFalloff * Time.deltaTime;

        if (damage <= 0)
            damage = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<EnemyHealthSystem>().DoDamage(damage);
            player.GetComponent<PlayerHealthSystem>().RechargeGem(gemRechargeAmount);
        }
        else if (other.gameObject.tag == "PlayerHit" || other.gameObject.tag == "Player")
            return;
        Destroy(gameObject);
    }

    public void InitialiseArrowValues(GameObject player, GameObject target, float damage, float gemRechargeAmount, float trackingStrength)
    {
        this.player = player;
        this.target = target;
        stateManager = player.gameObject.GetComponent<PlayerStateManager>();

        this.damage = damage;
        this.gemRechargeAmount = gemRechargeAmount;
        this.trackingStrength = trackingStrength;

        moveSpeed = stateManager.arrowMoveSpeed;
        damageFalloff = stateManager.damageFalloff;
    }
}