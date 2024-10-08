using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletion : Singleton<LevelCompletion>
{
    private GameObject _victoryImage;
    private GameObject _loseImage;
    [SerializeField] private Button[] _homeButtons;
    [SerializeField] private Button _retryButton;

    [SerializeField] AudioClip _victorySound;
    [SerializeField] AudioClip _loseSound;

    [SerializeField] private AudioSource _audioSource;

    [Header("Full Stars")]
    [SerializeField] private GameObject[] _fullStars;
    //[SerializeField] private GameObject _defeatImage;

    private void Start()
    {

        _victoryImage = transform.GetChild(0).gameObject;
        _loseImage = transform.GetChild(1).gameObject;

        foreach (var button in _homeButtons)
        {
            button.onClick.AddListener(() => { SceneLoadingManager.Instance.LoadWorldMapScene(); });
        }

        _retryButton.onClick.AddListener(() => { SceneLoadingManager.Instance.LoadCurrentScene(); });
    }

    public void Victory(int fullStarAmount)
    {
        if (_audioSource == null)
        {
            _audioSource = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        }
        _loseImage.SetActive(false);
        _victoryImage.SetActive(true);
        //GameManager.Instance.OnLevelCompleted();

       // Time.timeScale = 0;
        if (_audioSource == null)
        {
            _audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        }
        _audioSource.Stop();
        _audioSource.loop = false;
        _audioSource.PlayOneShot(_victorySound);

        StartCoroutine(ShowStars(fullStarAmount));

    }

    IEnumerator ShowStars(int fullStarAmount)
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < fullStarAmount; i++)
        {
            _fullStars[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        Time.timeScale = 0;
    }

    public void GameOver()
    {
        if (_audioSource == null)
        {
            _audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        }
        Time.timeScale = 0;
        _audioSource.Stop();
        _audioSource.loop = false;
        _audioSource.PlayOneShot(_loseSound);

        Debug.Log("Game Over");
        _victoryImage.SetActive(false);
        _loseImage.SetActive(true);
    }

}
