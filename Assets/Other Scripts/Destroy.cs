using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject barrier;
    
    public void OnTriggerEnter(Collider Player)
    {
        GameObject.Find("Menu").GetComponent<UIController>().OpenTutorial();

        Destroy(barrier);
        Destroy(gameObject);
    }
}
