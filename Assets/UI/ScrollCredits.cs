using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollCredits : MonoBehaviour
{
    RectTransform creditsTransform;
    public float scrollSpeed;
    public float fastScrollSpeed;
    public float endCreditsPos;
    public AudioSource creditsAudioSource;

    private void Start()
    {
        creditsTransform = GetComponent<RectTransform>();

        GameObject.Find("Player").GetComponent<PlayerStateManager>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerHealthSystem>().enabled = false;
        GameObject.Find("Menu").GetComponent<UIController>().enabled = false;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
            audioSource.enabled = false;

        creditsAudioSource.enabled = true;
        
    }

    void Update()
    {
        if (creditsTransform.position.y >= endCreditsPos)
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetAxis("Jump") > 0 && creditsTransform.position.y < endCreditsPos)
            creditsTransform.position = new Vector3(creditsTransform.position.x, creditsTransform.position.y + fastScrollSpeed * Time.deltaTime, creditsTransform.position.z);
        else
            creditsTransform.position = new Vector3(creditsTransform.position.x, creditsTransform.position.y + scrollSpeed * Time.deltaTime, creditsTransform.position.z);

    }
}
