using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Screens")]
    public GameObject settingsScreen;
    public GameObject gameplaySettingsScreen;
    public GameObject graphicsSettingsScreen;
    public GameObject audioSettingsScreen;
    public GameObject controlsScreen;

    [Header("First Objects")]
    public GameObject firstSettingsObject;
    public GameObject firstGameplayObject;
    public GameObject firstGraphicsObject;
    public GameObject firstAudioObject;
    public GameObject firstControlsObject;



    [Header("Gameplay Settings")]
    public Slider fovSlider;
    public int fovValue;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenGameplaySettings()
    {

    }

    public void OpenGraphicsSettings()
    {

    }

    public void OpenControls()
    {

    }

    void UpdateFov()
    {

    }
}
