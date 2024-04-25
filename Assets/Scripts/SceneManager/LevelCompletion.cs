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
        _loseImage.SetActive(false);
        _victoryImage.SetActive(true);
        GameManager.Instance.OnLevelCompleted();
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
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        _victoryImage.SetActive(false);
        _loseImage.SetActive(true);
    }

}
