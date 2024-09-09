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
    public float maxHealth;

    private void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
    }

    public void DoDamage(float damage)
    {
        healthBar.fillAmount -= damage * (1 / maxHealth);
        if (healthBar.fillAmount == 0)
        {
            if (!stateManager.isInSecondPhase)
            {
                stateManager.isInSecondPhase = true;
                TransitionToSecondPhase();
            }
            else
            {
                Destroy(gameObject);
                camCont.EndLockOn();
                victoryScreen.SetActive(true);
            }
        }
    }

    private void TransitionToSecondPhase()
    {
        healthBar.fillAmount = maxHealth;
        stateManager.meshRenderer.material.color = stateManager.phase2Colour;
    }
}
