using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _backgroundMusicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    private void Start()
    {
        SetBackgroundMusicVolume();
        SetSFXVolume();
    }


    public void SetBackgroundMusicVolume()
    {
        float volume = _backgroundMusicVolumeSlider.value;
        _audioMixer.SetFloat("BackgroundMusic", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = _sfxVolumeSlider.value;
        _audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);
    }
}
