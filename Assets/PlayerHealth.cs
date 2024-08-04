using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;

    public void DoDamage(float damage)
    {
        healthBar.value -= damage;
        if (healthBar.value <= 0)
        {
            healthBar.value = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
