using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    public CameraController camCont;
    public Image healthBar;
    public float maxHealth;

    public void DoDamage(float damage)
    {
        healthBar.fillAmount -= damage * (1 / maxHealth);
        if (healthBar.fillAmount == 0)
        {
            Destroy(gameObject);
            camCont.EndLockOn();
        }
    }
}
