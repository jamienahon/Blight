using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iMPACTFIRE : MonoBehaviour
{
    public Animation VFX;


    void Update ()
    {
       if(Input.GetKey(KeyCode.L))
        {
            VFX.Play();
            Debug.Log("HEYO");
        }
    }
}
