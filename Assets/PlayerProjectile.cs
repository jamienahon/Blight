using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public GameObject target;
    public float trackingStrength;
    public float damage;

    private void Update()
    {
        if (target)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <
                Vector3.Distance(player.transform.position, target.transform.position))
            {
                Vector3 delta = (target.transform.position - transform.position).normalized;
                transform.up = Vector3.RotateTowards(transform.up, delta, trackingStrength * Time.deltaTime, 0.0f);
            }
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthSystem>().DoDamage(damage);
        }
        else if (other.gameObject.tag == "PlayerHit" || other.gameObject.tag == "Player")
            return;
        Destroy(gameObject);
    }
}
