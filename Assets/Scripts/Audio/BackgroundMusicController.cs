using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] AudioClip[] _backgroundMusic;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = GetSceneBackgroundMusic();
        _audioSource.Play();
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
