using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class UIMenu
{
    UIMenu()
    {
        selectedUIElement = firstUIElement;
    }

    public GameObject menuScreen;
    public GameObject firstUIElement;
    [HideInInspector] public GameObject selectedUIElement;
}

public class UIController : MonoBehaviour
{
    public UIMenu mainMenu, settingsMenu, gameplaySettingsMenu, graphicsSettingsMenu, audioSettingsMenu,
        controlsScreenMenu;

    public GameObject tutorialScreen;

    UIMenu currentMenu = null;
    UIMenu previousMenu = null;

    CursorLockMode cursorLockMode = CursorLockMode.Locked;
    bool isCursorVisible = false;

    bool hasClicked = false;

    private void Update()
    {
        Cursor.lockState = cursorLockMode;
        Cursor.visible = isCursorVisible;

        if (currentMenu != null)
            currentMenu.selectedUIElement = EventSystem.current.currentSelectedGameObject;

        if (Input.GetAxisRaw("Pause") > 0 && !hasClicked)
        {
            hasClicked = true;


            if (tutorialScreen.activeSelf)
            {
                tutorialScreen.SetActive(false);
                Time.timeScale = 1;
            }
            else if (currentMenu == null)
                OpenMenu(mainMenu.menuScreen);
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

    public void OpenMenu(GameObject menu)
    {
        Time.timeScale = 0;
        SetCursorMode(CursorLockMode.None, true);

        if (currentMenu != null)
            currentMenu.menuScreen.SetActive(false);

        if (menu == mainMenu.menuScreen)
        {
            currentMenu = mainMenu;
            previousMenu = null;
        }
        else if (menu == settingsMenu.menuScreen)
        {
            currentMenu = settingsMenu;
            previousMenu = mainMenu;
        }
        else if (menu == gameplaySettingsMenu.menuScreen)
        {
            currentMenu = gameplaySettingsMenu;
            previousMenu = settingsMenu;
        }
        else if (menu == graphicsSettingsMenu.menuScreen)
        {
            currentMenu = graphicsSettingsMenu;
            previousMenu = settingsMenu;
        }
        else if (menu == audioSettingsMenu.menuScreen)
        {
            currentMenu = audioSettingsMenu;
            previousMenu = settingsMenu;
        }
        else if (menu == controlsScreenMenu.menuScreen)
        {
            currentMenu = controlsScreenMenu;
            previousMenu = gameplaySettingsMenu;
        }

        currentMenu.menuScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(currentMenu.selectedUIElement);
    }

    public void GoBack()
    {
        if (previousMenu != null)
        {
            currentMenu.selectedUIElement = currentMenu.firstUIElement;
            OpenMenu(previousMenu.menuScreen);
        }
        else
        {
            currentMenu.menuScreen.SetActive(false);
            currentMenu = null;
            Time.timeScale = 1;
            SetCursorMode(CursorLockMode.Locked, false);
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