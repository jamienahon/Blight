using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealthSystem : MonoBehaviour
{
    PlayerStateManager stateManager;
    public float maxHealth;
    public Image healthBar;
    public Slider staminaBar;
    public TextMeshProUGUI healthChargesText;
    public int healthCharges;
    public int healAmount;
    bool hasClicked = false;
    public float healCooldown;
    float canHeal;

    void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    public void DoDamage(float damage)
    {
        if (stateManager.isInvincible)
            return;
        else
            stateManager.SwitchState(stateManager.getHitState);

        healthBar.fillAmount -= damage * (1 / maxHealth);
        if (healthBar.fillAmount <= 0)
        {
            healthBar.fillAmount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Heal(float healAmount)
    {
        canHeal = Time.time + healCooldown;
        if (healthCharges <= 0)
            return;

        healthBar.fillAmount += healAmount * (1 / maxHealth);
        healthCharges--;
    }

    private void Update()
    {
        healthChargesText.text = healthCharges.ToString();
        if(Input.GetAxis("Heal") > 0 && !hasClicked && Time.time >= canHeal)
        {
            hasClicked = true;
            Heal(healAmount);
        }

        if (Input.GetAxis("Heal") == 0)
            hasClicked = false;
    }
}
