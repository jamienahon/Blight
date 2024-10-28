using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSystem : MonoBehaviour
{
    EnemyStateManager stateManager;
    public CameraController camCont;
    public GameObject victoryScreen;
    public Image healthBar;
    public Image healthBarBackground;
    public float maxHealth;
    public Animation victoryDoor;
    


    [Header("BarSmoothing")]
    public float healthBarSmoothDelay;
    float startHealthBarSmoothing;
    public float healthBarSmoothSpeed;
    bool healthBarSmoothing = false;



    private void Start()
    {
        stateManager = GetComponent<EnemyStateManager>(); 
    }

    private void Update()
    {
        if (Time.time >= startHealthBarSmoothing && healthBarSmoothing)
            SmoothHealthBar();
    }


    public void DoDamage(float damage)
    {
        healthBar.fillAmount -= damage * (1 / maxHealth);

        if (!healthBarSmoothing)
        {
            healthBarSmoothing = true;
            startHealthBarSmoothing = Time.time + healthBarSmoothDelay;
        }

        //ALL COMMENTS ARE SECOND PHASE RELATED


        if (healthBar.fillAmount == 0)
         //{
         //   if (!stateManager.isInSecondPhase)
         //     {
         //         stateManager.isInSecondPhase = true;
         //         TransitionToSecondPhase();
         //     }
              
            {
            stateManager.SwitchState(stateManager.EnemyDeathState);
    //        camCont.EndLockOn();
      //      victoryDoor.Play();
        //    victoryScreen.SetActive(true);
        }
         //    }
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

   //  private void TransitionToSecondPhase()
   //   {
   //     healthBar.fillAmount = maxHealth;
   //     stateManager.meshRenderer.material.color = stateManager.phase2Colour;
   // }
}
