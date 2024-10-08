using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealthSystem : MonoBehaviour
{
    PlayerStateManager stateManager;
    public GameObject deathScreen;
    public Animator healingChargesAnim;

    [Header("Health")]
    public Image healthBar;
    public Image healthBarBackground;
    public float maxHealth;

    [Header("Stamina")]
    public Image staminaBar;
    public Image staminaBarBackground;
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

    [Header("BarSmoothing")]
    public float healthBarSmoothDelay;
    public float staminaBarSmoothDelay;
    float startHealthBarSmoothing;
    float startStaminaBarSmoothing;
    public float healthBarSmoothSpeed;
    public float staminaBarSmoothSpeed;
    bool healthBarSmoothing = false;
    bool staminaBarSmoothing = false;

    void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    private void Update()
    {
        if (Time.time >= refillStamina && staminaBar.fillAmount < 1)
            RefillStamina();

        if (rechargeGem && healingGemImages[0].fillAmount == 1)
        {
            healingChargesAnim.playableGraph.Play();
            healingChargesAnim.Play("Recharge Gem");
            healingGemImages[0].color = new Color(1, 1, 1, 1);
            rechargeGem = false;
            healthCharges = 1;
        }

        if (Time.time >= startStaminaBarSmoothing && staminaBarSmoothing)
            SmoothStaminaBar();

        if (Time.time >= startHealthBarSmoothing && healthBarSmoothing)
            SmoothHealthBar();

        if (healingChargesAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && healingChargesAnim.GetCurrentAnimatorStateInfo(0).IsName("Use Gem 1"))
        {
            healingChargesAnim.playableGraph.Stop();
        }
    }

    public void DoDamage(float damage)
    {
        if (stateManager.isInvincible)
            return;


        healthBar.fillAmount -= damage * (1 / maxHealth);
        stateManager.SwitchState(stateManager.getHitState);

        if (!healthBarSmoothing)
        {
            healthBarSmoothing = true;
            startHealthBarSmoothing = Time.time + healthBarSmoothDelay;
        }

        if (healthBar.fillAmount <= 0)
        {
            healthBar.fillAmount = 0;
            deathScreen.SetActive(true);
            stateManager.gameObject.SetActive(false);
            CameraController camCont = Camera.main.GetComponent<CameraController>();
            camCont.followCam.enabled = false;
        }
    }

    void SmoothHealthBar()
    {
        healthBarBackground.fillAmount -= healthBarSmoothSpeed * Time.deltaTime;
        if (healthBarBackground.fillAmount <= healthBar.fillAmount)
        {
            healthBarBackground.fillAmount = healthBar.fillAmount;
            healthBarSmoothing = false;
        }
    }

    public void Heal(float healAmount)
    {
        if (healthCharges <= 0)
            return;

        healthBar.fillAmount += healAmount * (1 / maxHealth);

        healingChargesAnim.Play("Use Gem " + healthCharges.ToString());

        if (!healthBarSmoothing)
            healthBarBackground.fillAmount = healthBar.fillAmount;

        if (healingGemImages.Count > 1)
        {
            healingGemImages.RemoveAt(0);
        }
        else
        {
            healingGemImages[0].fillAmount = 0.999f;
            rechargeGem = true;
        }

        healthCharges--;
    }

    public void ConsumeStamina(float consumeAmount)
    {
        if (staminaBar.fillAmount > 0)
        {
            refillStamina = Time.time + timeBeforeStaminaRefill;
            staminaBar.fillAmount -= consumeAmount * (1 / maxStamina);

            if (!staminaBarSmoothing)
            {
                staminaBarSmoothing = true;
                startStaminaBarSmoothing = Time.time + staminaBarSmoothDelay;
            }
        }
    }

    void SmoothStaminaBar()
    {
        staminaBarBackground.fillAmount -= staminaBarSmoothSpeed * Time.deltaTime;
        if (staminaBarBackground.fillAmount <= staminaBar.fillAmount)
        {
            staminaBarBackground.fillAmount = staminaBar.fillAmount;
            staminaBarSmoothing = false;
        }
    }

    void RefillStamina()
    {
        staminaBar.fillAmount += normalStamRefillSpeed * (1 / maxStamina) * Time.deltaTime;

        if (!staminaBarSmoothing)
            staminaBarBackground.fillAmount = staminaBar.fillAmount;
    }

    public void RechargeGem(float recharge)
    {
        healingGemImages[0].fillAmount += recharge * (1 / maxRecharge);
    }
}