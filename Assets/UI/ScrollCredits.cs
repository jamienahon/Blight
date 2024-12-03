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

    private void Start()
    {
        creditsTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (creditsTransform.position.y >= endCreditsPos)
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetAxis("Jump") > 0)
            creditsTransform.position = new Vector3(creditsTransform.position.x, creditsTransform.position.y + fastScrollSpeed * Time.deltaTime, creditsTransform.position.z);
        else
            creditsTransform.position = new Vector3(creditsTransform.position.x, creditsTransform.position.y + scrollSpeed * Time.deltaTime, creditsTransform.position.z);

    }
}
