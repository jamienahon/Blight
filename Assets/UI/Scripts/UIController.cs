using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    CameraController camController;

    public GameObject background, mainMenu, settingsMenu, gameplaySettings, graphicsSettings, audioSettings;
    bool isInMenus;


    private void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isInMenus)
            {
                OpenMainMenu();
            }
            else
            {
                GoBack();
            }
        }
    }

    private void SetCursorMode(CursorLockMode lockMode, bool isVisible)
    {
        camController.cursorLockMode = lockMode;
        camController.isCursorVisible = isVisible;
    }

    public void GoBack()
    {
        if(mainMenu.activeSelf)
        {
            CloseMainMenu();
        }
        else if(settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            OpenMainMenu();
        }
        else if(gameplaySettings.activeSelf)
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
        isInMenus = true;
        SetCursorMode(CursorLockMode.Confined, true);

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
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenGameplaySettings()
    {
        settingsMenu.SetActive(false);
        gameplaySettings.SetActive(true);
    }

    public void OpenGraphicsSettings()
    {
        settingsMenu.SetActive(false);
        graphicsSettings.SetActive(true);
    }

    public void OpenAudioSettings()
    {
        settingsMenu.SetActive(false);
        audioSettings.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
