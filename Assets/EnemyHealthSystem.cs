using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    public PlayerStateManager stateManager;
    public CameraController camCont;
    public Slider healthBar;

    public void DoDamage(float damage)
    {
        healthBar.value -= damage;
        if (healthBar.value <= 0)
        {
            healthBar.value = 0;
            Destroy(gameObject);
            stateManager.isLockedOn = false;
            camCont.EndLockOn();
        }
    }
}
