using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerRespawn : MonoBehaviour
{
    PlayerStateManager stateManager;
    public AudioSource Hurt; 
    public float fallThreshold = -10f; // The Y value below which the player will respawn
    public GameObject deathScreen;
    public Image healthBar;



    void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        // Check if the player's Y position is below the threshold
        if (transform.position.y < fallThreshold)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<PlayerHealthSystem>();
        healthBar.fillAmount = 0;
        deathScreen.SetActive(true);
        stateManager.gameObject.SetActive(false);
        CameraController camCont = Camera.main.GetComponent<CameraController>();
        camCont.followCam.enabled = false;

       // SceneManager.LoadScene(1);

    }
}
