using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject barrier;
    public EnemyStateManager enemyStateManager;
    public AudioSource Phase1;
    public AudioSource Ambience;

    private void Start()
    {
        enemyStateManager.enabled = false;
    }

    public void OnTriggerEnter(Collider Player)
    {
        // GameObject.Find("Menu").GetComponent<UIController>().OpenTutorial();
        enemyStateManager.enabled = true;




        // PlaySounds
        Phase1.Play();
        Ambience.Stop();

        //Destroy(barrier);

    
    
    
       // GameObject.Find("Menu").GetComponent<UIController>().OpenTutorial();

        Destroy(barrier);
        Destroy(gameObject);
    }
}
