using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject background, mainMenu, settingsMenu, gameplaySettings, graphicsSettings, audioSettings;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
            background.SetActive(mainMenu.activeSelf);

            if(mainMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }

    public class MenuController
    {
        
    }
}
