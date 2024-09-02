using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject barrier;
    
    public void OnTriggerEnter(Collider Player)
    {
        Destroy(barrier);

    }

  
}
