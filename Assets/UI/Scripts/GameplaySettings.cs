using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class GameplaySettings : MonoBehaviour
{
    PersistantData persistantData;

    public CinemachineFreeLook freeLookCam;

    public Slider fovSlider;
    public TextMeshProUGUI fovValue;

    private void Start()
    {
        persistantData = GameObject.Find("PersistantData").GetComponent<PersistantData>();
        InitialiseGameplaySettings();
    }

 //   private void Update()
 //   {
 //       UpdateFOV();
 //   }

    private void InitialiseGameplaySettings()
    {
        fovSlider.value = persistantData.fovValue;
    }

    private void UpdateFOV()
    {
        persistantData.fovValue = fovSlider.value;
        freeLookCam.m_Lens.FieldOfView = fovSlider.value;
        fovValue.text = fovSlider.value.ToString();
    }
}
