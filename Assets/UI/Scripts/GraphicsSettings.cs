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

        resolutionDropdown.value = persistantData.resolutionDropdownValue;
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
                Screen.SetResolution(1600, 900, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 2)
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 3)
                Screen.SetResolution(2048, 1152, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 4)
                Screen.SetResolution(2560, 1080, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 5)
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 6)
                Screen.SetResolution(3440, 1440, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 7)
                Screen.SetResolution(2840, 2160, Screen.fullScreen);
            else if (currentResolutuionDropdownValue == 8)
                Screen.SetResolution(5120, 2160, Screen.fullScreen);

        }
    }
}
