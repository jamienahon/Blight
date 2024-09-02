using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UIElements;


public class SceneChange : MonoBehaviour
{
    public AudioSource Clicker;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);// this can load the scene number, scene number is what scene is in build menu

    }
    public void QuitGame()
    {

        Application.Quit();
    }

    public void Clicking()
    {
        Clicker.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);// this can load the scene number, scene number is what scene is in build menu

    }

    public void Restartgame()
    {
        SceneManager.LoadScene(1);
    }
}
