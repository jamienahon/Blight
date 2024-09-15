using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    PersistantData persistantData;

    [Header("AudioSources")]
    public List<AudioSource> allAudioSources;
    public AudioSource musicAudioSource;
    public List<AudioSource> sfxAudioSources;

    [Header("Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Text")]
    public TextMeshProUGUI masterVolumeValue;
    public TextMeshProUGUI musicVolumeValue;
    public TextMeshProUGUI sfxVolumeValue;

    private void Start()
    {
        persistantData = GameObject.Find("PersistantData").GetComponent<PersistantData>();

        FindAudioSources();
        InitialiseAudioSettings();
    }

    private void Update()
    {
        UpdateMasterVolume();
        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    private void InitialiseAudioSettings()
    {
        masterVolumeSlider.value = persistantData.masterVolume;

        musicVolumeSlider.value = persistantData.musicVolumeSliderValue;
        musicAudioSource.volume = persistantData.musicVolume;

        sfxVolumeSlider.value = persistantData.sfxVolumeSliderValue;
        foreach (AudioSource audioSource in sfxAudioSources)
            audioSource.volume = persistantData.sfxVolume;
    }

    public void FindAudioSources()
    {
        allAudioSources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        musicAudioSource = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
        sfxAudioSources = new List<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.gameObject.tag != "BackgroundAudioSource")
                sfxAudioSources.Add(audioSource);
        }
    }

    public void UpdateMasterVolume()
    {
        UpdateMusicVolume();
        UpdateSFXVolume();
        persistantData.masterVolume = masterVolumeSlider.value;
        masterVolumeValue.text = Mathf.RoundToInt(masterVolumeSlider.value * 100).ToString();
    }

    public void UpdateMusicVolume()
    {
        musicAudioSource.volume = musicVolumeSlider.value * masterVolumeSlider.value;
        persistantData.musicVolume = musicAudioSource.volume;
        persistantData.musicVolumeSliderValue = musicVolumeSlider.value;
        musicVolumeValue.text = Mathf.RoundToInt(musicVolumeSlider.value * 100).ToString();
    }

    public void UpdateSFXVolume()
    {
        foreach (AudioSource audioSource in sfxAudioSources)
            audioSource.volume = sfxVolumeSlider.value * masterVolumeSlider.value;
        persistantData.sfxVolume = sfxAudioSources[0].volume;
        persistantData.sfxVolumeSliderValue = sfxVolumeSlider.value;
        sfxVolumeValue.text = Mathf.RoundToInt(sfxVolumeSlider.value * 100).ToString();
    }
}
