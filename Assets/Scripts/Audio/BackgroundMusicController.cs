using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] _backgroundMusic;

    private AudioSource _audioSource;
    [SerializeField] public AudioMixer _audioMixer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = GetSceneBackgroundMusic();
        _audioSource.Play();
        //VolumeSettings.Instance.SetBackgroundMusicVolume();
        //VolumeSettings.Instance.SetSFXVolume();
        if (_audioMixer != null)
        {
            _audioMixer.SetFloat("BackgroundMusic", Mathf.Log10(PlayerPrefs.GetFloat("BackgroundMusic")) * 20);
            _audioMixer.SetFloat("Sfx", Mathf.Log10(PlayerPrefs.GetFloat("Sfx")) * 20);
        }
        
    }

    //This needs to be Updated once the scenes are implemented!!
    // Remember to put the audio controller prefab!!
    private AudioClip GetSceneBackgroundMusic()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Scene name int: " + scene);
        switch (scene)
        {
            case 0:
                return _backgroundMusic[0];
            case 1:
                return _backgroundMusic[0];
            case 2:
                return _backgroundMusic[1];
            case 3:
                return _backgroundMusic[2];
            default:
                return _backgroundMusic[_backgroundMusic.Length - 1];               
        }
    }

}
