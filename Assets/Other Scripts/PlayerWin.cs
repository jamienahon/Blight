using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public GameObject credits;
    public void OnTriggerEnter(Collider Player)
    {
        GameObject.Find("Player").GetComponent<PlayerStateManager>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerHealthSystem>().enabled = false;
        GameObject.Find("Menu").GetComponent<UIController>().enabled = false;
        credits.SetActive(true);
    }
}
