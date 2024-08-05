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
    public float maxStamina;
    public float staminaRefillSpeed;
    public Image healthBar;
    public Image staminaBar;
    public TextMeshProUGUI healthChargesText;
    public int healthCharges;
    public int healAmount;
    bool hasClicked = false;
    public float healCooldown;
    float canHeal;

    public float timeBeforeStaminaRefill;
    float refillStamina;

    void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }
    private void Update()
    {
        healthChargesText.text = healthCharges.ToString();
        if (Input.GetAxis("Heal") > 0 && !hasClicked && Time.time >= canHeal)
        {
            hasClicked = true;
            Heal(healAmount);
        }

        if (Input.GetAxis("Heal") == 0)
            hasClicked = false;

        if (Time.time >= refillStamina && staminaBar.fillAmount < 1)
            RefillStamina();
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

    public void ConsumeStamina(float consumeAmount)
    {
        if (staminaBar.fillAmount > 0)
        {
            refillStamina = Time.time + timeBeforeStaminaRefill;
            staminaBar.fillAmount -= consumeAmount * (1 / maxStamina);
        }
    }

    void RefillStamina()
    {
        staminaBar.fillAmount += staminaRefillSpeed * Time.deltaTime;
    }
}
