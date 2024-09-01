using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public GameObject target;
    public float trackingStrength;

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
            other.gameObject.GetComponent<EnemyHealthSystem>().DoDamage(80);            //this is light projectile dmg
        }
        else if (other.gameObject.tag == "PlayerHit" || other.gameObject.tag == "Player")
            return;
        Destroy(gameObject);
    }
}
