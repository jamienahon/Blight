using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public GameObject credits;


    //public AudioSource Steps;
   // public AudioSource Steps2;
  public AudioSource Music;
 //   public AudioSource StopMusic;
 //   public AudioSource Ambience;
 //   public AudioSource Lightning;

    public void OnTriggerEnter(Collider Player)
    {
        credits.SetActive(true);
    //    Steps.Stop();
    //    Steps2.Stop();
       // StopMusic.Stop();
       Music.Play();
      //  Ambience.Stop();
     //   Lightning.Stop();


    }
}
