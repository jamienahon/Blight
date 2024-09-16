using UnityEngine;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
    PersistantData persistantData;

    public TMP_Dropdown screenModeDropdown;
    public int currentScreenModeValue;

    public TMP_Dropdown resolutionDropdown;
    public int currentResolutuionDropdownValue;

    private void Awake()
    {
        persistantData = GameObject.Find("PersistantData").GetComponent<PersistantData>();
        InitialiseGraphicsSettings();
    }

    private void Update()
    {
        UpdateScreenMode();
        UpdateResolution();
    }

    private void InitialiseGraphicsSettings()
    {
        screenModeDropdown.value = persistantData.screenModeValue;
        currentScreenModeValue = screenModeDropdown.value;
    }

    private void UpdateScreenMode()
    {
        if(screenModeDropdown.value != currentScreenModeValue)
        {
            currentScreenModeValue = screenModeDropdown.value;
            persistantData.screenModeValue = screenModeDropdown.value;

            if (currentScreenModeValue == 0)
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            else if (currentScreenModeValue == 1)
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            else if (currentScreenModeValue == 2)
                Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void UpdateResolution()
    {
        if(resolutionDropdown.value != currentResolutuionDropdownValue)
        {
            currentResolutuionDropdownValue = resolutionDropdown.value;
            persistantData.resolutionDropdownValue = resolutionDropdown.value;

            if (currentResolutuionDropdownValue == 0)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 1)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 2)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 3)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 4)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 5)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 6)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 7)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 8)
                Screen.SetResolution(1280, 720, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 9)
                Screen.SetResolution(1280, 720, Screen.fullScreen);

        }
    }
}
