using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    public Animation Closetutorial;
    public void OnTriggerEnter(Collider Player)
    {
    GameObject.Find("Menu").GetComponent<UIController>().OpenTutorial();
    }
    public void OnTriggerExit(Collider other)
    {
      Closetutorial.Play();
      GameObject.Find("Menu").GetComponent<UIController>().CloseTutorial ();
    }
   
}
