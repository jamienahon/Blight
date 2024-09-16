using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public EventSystem eventSystem;

    public GameObject background, mainMenu, settingsMenu, gameplaySettings, graphicsSettings, audioSettings,
         tutorialScreen, controlsScreen;

    bool hasClicked = false;

    public GameObject currentMenu;
    public GameObject previousMenu;

    CursorLockMode cursorLockMode;
    bool isCursorVisible;

    private void Update()
    {
        Cursor.lockState = cursorLockMode;
        Cursor.visible = isCursorVisible;

        if (Input.GetAxisRaw("Pause") > 0 && !hasClicked)
        {
            hasClicked = true;

            if (currentMenu == null)
                OpenMenu(mainMenu);
            else
                GoBack();
        }

        if (Input.GetAxisRaw("Pause") == 0)
            hasClicked = false;
    }

    private void SetCursorMode(CursorLockMode lockMode, bool isVisible)
    {
        cursorLockMode = lockMode;
        isCursorVisible = isVisible;

    }

    public void GoBack()
    {
        currentMenu.SetActive(false);

        if (previousMenu)
        {
            OpenMenu(previousMenu);
        }
        else
        {
            currentMenu = null;
            Time.timeScale = 1;
            SetCursorMode(CursorLockMode.Locked, false);
        }
    }

    public void OpenMenu(GameObject menu)
    {
        Time.timeScale = 0;
        SetCursorMode(CursorLockMode.None, true);

        if (menu == mainMenu)
        {
            currentMenu = mainMenu;
            previousMenu = null;
            mainMenu.SetActive(true);
        }
        else if(menu == settingsMenu)
        {
            currentMenu = settingsMenu;
            previousMenu = mainMenu;
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        else if(menu == gameplaySettings)
        {
            currentMenu = gameplaySettings;
            previousMenu = settingsMenu;
            settingsMenu.SetActive(false);
            gameplaySettings.SetActive(true);
        }
        else if(menu == graphicsSettings)
        {
            currentMenu = graphicsSettings;
            previousMenu = settingsMenu;
            settingsMenu.SetActive(false);
            graphicsSettings.SetActive(true);
        }
        else if(menu == audioSettings)
        {
            currentMenu = audioSettings;
            previousMenu = settingsMenu;
            settingsMenu.SetActive(false);
            audioSettings.SetActive(true);
        }
        else if(menu == controlsScreen)
        {
            currentMenu = controlsScreen;
            previousMenu = gameplaySettings;
            gameplaySettings.SetActive(false);
            controlsScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void OpenTutorial()
    {
        tutorialScreen.SetActive(true);
        Time.timeScale = 0;
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