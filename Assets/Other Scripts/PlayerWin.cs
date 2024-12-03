using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public GameObject credits;
    public void OnTriggerEnter(Collider Player)
    {
        credits.SetActive(true);
    }
}
