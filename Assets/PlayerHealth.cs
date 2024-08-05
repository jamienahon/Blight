using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public Slider staminaBar;
    public TextMeshProUGUI healthChargesText;
    public int healthCharges;
    public int healAmount;
    bool hasClicked = false;

    public void DoDamage(float damage)
    {
        healthBar.value -= damage;
        if (healthBar.value <= 0)
        {
            healthBar.value = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Heal(float healAmount)
    {
        if (healthCharges <= 0)
            return;

        healthBar.value += healAmount;
        healthCharges--;
        if (healthBar.value >= healthBar.maxValue)
        {
            healthBar.value = healthBar.maxValue;
        }
    }

    private void Update()
    {
        healthChargesText.text = healthCharges.ToString();
        if(Input.GetAxis("Heal") > 0 && !hasClicked)
        {
            hasClicked = true;
            Heal(healAmount);
        }

        if (Input.GetAxis("Heal") == 0)
            hasClicked = false;
    }
}
