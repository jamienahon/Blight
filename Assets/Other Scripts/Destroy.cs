using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject barrier;
    public EnemyStateManager enemyStateManager;

    private void Start()
    {
        enemyStateManager.enabled = false;
    }

    public void OnTriggerEnter(Collider Player)
    {
        // GameObject.Find("Menu").GetComponent<UIController>().OpenTutorial();
        enemyStateManager.enabled = true;

        //Destroy(barrier);
        Destroy(gameObject);
    }
}
