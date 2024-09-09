using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public static PersistantData instance;

    [Header("Audio Settings")]
    public float masterVolume;
    public float musicVolume;
    public float musicVolumeSliderValue;
    public float sfxVolume;
    public float sfxVolumeSliderValue;

    private void Awake()
    {
        PreventDuplication();
    }

    private void PreventDuplication()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
