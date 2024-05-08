using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : Singleton<VolumeSettings>
{
    [SerializeField] public AudioMixer _audioMixer;
    [SerializeField] private Slider _backgroundMusicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    private void Start()
    {
        SetBackgroundMusicVolume();
        SetSFXVolume();
    }


    public void SetBackgroundMusicVolume()
    {
        if (_backgroundMusicVolumeSlider != null)
        {
            PlayerPrefs.SetFloat("BackgroundMusic", _backgroundMusicVolumeSlider.value);
            float volume = PlayerPrefs.GetFloat("BackgroundMusic");
            _audioMixer.SetFloat("BackgroundMusic", Mathf.Log10(volume) * 20);
        }
        else
        {
            float volume = PlayerPrefs.GetFloat("BackgroundMusic");
            _audioMixer.SetFloat("BackgroundMusic", Mathf.Log10(volume) * 20);
        }
    }

    public void SetSFXVolume()
    {
        if (_sfxVolumeSlider != null)
        {
            PlayerPrefs.SetFloat("Sfx", _sfxVolumeSlider.value);
            float volume = PlayerPrefs.GetFloat("Sfx");
            _audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);
        }
        else
        {
            float volume = PlayerPrefs.GetFloat("Sfx");
            _audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);
        }
        //float volume = PlayerPrefs.GetFloat("Sfx");
        //_audioMixer.SetFloat("Sfx", Mathf.Log10(volume) * 20);

    }
}
