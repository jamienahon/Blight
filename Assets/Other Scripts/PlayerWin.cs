using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public void OnTriggerEnter(Collider Player)
    {
        SceneManager.LoadScene(0);
    }
}
