using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    public Animation Ambience;


    private void OnTriggerExit(Collider other)
    {
        Ambience.Play();
    }


}
