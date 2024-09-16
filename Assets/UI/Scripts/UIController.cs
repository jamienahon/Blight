using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    CameraController camController;
    public EventSystem eventSystem;

    public GameObject background, mainMenu, settingsMenu, gameplaySettings, graphicsSettings, audioSettings;
    bool isInMenus;

    bool hasClicked = false;

    public GameObject firstMainMenuObject;
    public GameObject firstSettingsObject;
    public GameObject firstGameplayObject;
    public GameObject firstGraphicsObject;
    public GameObject firstAudioObject;


    private void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Pause") > 0 && !hasClicked)
        {
            hasClicked = true;
            if (!isInMenus)
            {
                OpenMainMenu();
            }
            else
            {
                GoBack();
            }
        }

        if (Input.GetAxisRaw("Pause") == 0)
            hasClicked = false;
    }

    private void SetCursorMode(CursorLockMode lockMode, bool isVisible)
    {
        camController.cursorLockMode = lockMode;
        camController.isCursorVisible = isVisible;
    }

    public void GoBack()
    {
        if (mainMenu.activeSelf)
        {
            CloseMainMenu();
        }
        else if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            OpenMainMenu();
        }
        else if (gameplaySettings.activeSelf)
        {
            gameplaySettings.SetActive(false);
            OpenSettings();
        }
        else if (graphicsSettings.activeSelf)
        {
            graphicsSettings.SetActive(false);
            OpenSettings();
        }
        else if (audioSettings.activeSelf)
        {
            audioSettings.SetActive(false);
            OpenSettings();
        }
    }

    public void OpenMainMenu()
    {
        eventSystem.SetSelectedGameObject(firstMainMenuObject);
        isInMenus = true;
        SetCursorMode(CursorLockMode.None, true);

        mainMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseMainMenu()
    {
        isInMenus = false;
        SetCursorMode(CursorLockMode.Locked, false);

        mainMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void OpenSettings()
    {
        eventSystem.SetSelectedGameObject(firstSettingsObject);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenGameplaySettings()
    {
        eventSystem.SetSelectedGameObject(firstGameplayObject);
        settingsMenu.SetActive(false);
        gameplaySettings.SetActive(true);
    }

    public void OpenGraphicsSettings()
    {
        eventSystem.SetSelectedGameObject(firstGraphicsObject);
        settingsMenu.SetActive(false);
        graphicsSettings.SetActive(true);
    }

    public void OpenAudioSettings()
    {
        eventSystem.SetSelectedGameObject(firstAudioObject);
        settingsMenu.SetActive(false);
        audioSettings.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void HomeScreen()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}