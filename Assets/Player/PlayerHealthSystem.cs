using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealthSystem : MonoBehaviour
{
    PlayerStateManager stateManager;

    [Header("Health")]
    public Image healthBar;
    public float maxHealth;

    [Header("Stamina")]
    public Image staminaBar;
    public float maxStamina;
    public float normalStamRefillSpeed;
    public float blockStamRefillSpeed;
    public float timeBeforeStaminaRefill;

    [Header("Healing")]
    public List<Image> healingGemImages;
    public int healthCharges;
    public float maxRecharge;

    float refillStamina;
    [HideInInspector] public bool rechargeGem = false;

    void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Time.time >= refillStamina && staminaBar.fillAmount < 1)
            RefillStamina();

        if (rechargeGem && healingGemImages[0].fillAmount == 1)
        {
            healingGemImages[0].color = new Color(1, 1, 1, 1);
            rechargeGem = false;
            healthCharges = 1;
        }
    }

    public void DoDamage(float damage)
    {
        if (stateManager.isInvincible)
            return;

        if (stateManager.currentState == stateManager.blockState)
        {
            healthBar.fillAmount -= (damage * (1 / maxHealth)) * stateManager.blockDamageReduction;
            stateManager.endBlockPause = Time.time + stateManager.blockPauseTime;
            ConsumeStamina(stateManager.blockStamCost);
        }
        else
        {
            healthBar.fillAmount -= damage * (1 / maxHealth);
            stateManager.SwitchState(stateManager.getHitState);
        }
        if (healthBar.fillAmount <= 0)
        {
            healthBar.fillAmount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Heal(float healAmount)
    {
        if (healthCharges <= 0)
            return;

        healthBar.fillAmount += healAmount * (1 / maxHealth);
        healthCharges--;

        if (healingGemImages.Count > 1)
        {
            healingGemImages[0].gameObject.SetActive(false);
            healingGemImages.RemoveAt(0);
        }
        else
        {
            rechargeGem = true;
            healingGemImages[0].color = new Color(1, 1, 1, 0.3f);
            healingGemImages[0].fillAmount = 0;
        }
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
        if (stateManager.currentState == stateManager.blockState)
            staminaBar.fillAmount += blockStamRefillSpeed * (1 / maxStamina) * Time.deltaTime;
        else
            staminaBar.fillAmount += normalStamRefillSpeed * (1 / maxStamina) * Time.deltaTime;
    }

    public void RechargeGem(float recharge)
    {
        healingGemImages[0].fillAmount += recharge * (1 / maxRecharge);
    }
}