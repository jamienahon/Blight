using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("AudioSources")]
    public List<AudioSource> allAudioSources;
    public AudioSource musicAudioSource;
    public List<AudioSource> sfxAudioSources;

    [Header("Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        allAudioSources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        musicAudioSource = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();

        sfxAudioSources = allAudioSources;
        foreach(AudioSource audioSource in sfxAudioSources)
        {
            if (audioSource.gameObject.tag == "BackgroundAudioSource")
                sfxAudioSources.Remove(audioSource);
        }
    }

    public void UpdateMasterVolume()
    {
        UpdateMusicVolume();
        UpdateSFXVolume();
    }

    public void UpdateMusicVolume()
    {
        musicAudioSource.volume = musicVolumeSlider.value * masterVolumeSlider.value;
    }

    public void UpdateSFXVolume()
    {
        foreach (AudioSource audioSource in sfxAudioSources)
            audioSource.volume = sfxVolumeSlider.value * masterVolumeSlider.value;
    }
}
