using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject enemy;
    public float moveSpeed;
    public GameObject target;
    public float trackingStrength;
    public float damage;

    private void Update()
    {
        if (target)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) <
                Vector3.Distance(enemy.transform.position, target.transform.position))
            {
                Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z);
                Vector3 delta = (targetPos - transform.position).normalized;
                transform.up = Vector3.RotateTowards(transform.up, delta, trackingStrength * Time.deltaTime, 0.0f);
            }
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<PlayerStateManager>().isInvincible)
                return;

            other.gameObject.GetComponent<PlayerHealthSystem>().DoDamage(damage);
        }
        else if (other.gameObject.tag == "EnemyHit" || other.gameObject.tag == "Enemy")
            return;
        Destroy(gameObject);
    }
}
