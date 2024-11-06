using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    
    public void OnTriggerEnter(Collider Player)
    {
    GameObject.Find("Menu").GetComponent<UIController>().OpenTutorial();
    }
    public void OnTriggerExit(Collider other)
    {
      
      GameObject.Find("Menu").GetComponent<UIController>().CloseTutorial ();
    }
   
}
