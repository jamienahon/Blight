using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject barrier;
    public EnemyStateManager enemyStateManager;
    public AudioSource Phase1;
    public AudioSource Ambience;
    public Animation BossHP;
    public Animation BossTheme;
    public Animation FogWall;

    private void Start()
    {
        enemyStateManager.enabled = false;
    }

    public void OnTriggerEnter(Collider Player)
    {
        // GameObject.Find("Menu").GetComponent<UIController>().OpenTutorial();
        BossHP.Play();
        enemyStateManager.enabled = true;

        // PlaySounds and Animations
        FogWall.Play();
        BossTheme.Play();
        Phase1.Play();
        Ambience.Stop();

        Object.Destroy(barrier);
    }
}
