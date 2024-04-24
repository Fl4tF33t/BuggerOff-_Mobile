using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics.CodeAnalysis;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int bugBits;

    //Santiago
    [SerializeField] private GameObject _levelEnd;
    

    public Action<int> HealthChange;
    public Action<int> BugBitsChange;

    public event Action OnUIChange;

    private PlayerData playerData;

    public int Health { get { return health; } }
    public int BugBits { get {  return bugBits; } }

    public int currentlyPlayingLevel;
    private JSONSaving saving;

    private void Start()
    {
        bugBits = 9000;

        WaveSystem.Instance.OnLevelCompleted += () => OnLevelCompleted(); 

        HealthChange = (amount) => { health += amount; OnUIChange?.Invoke();
            if (health <= 0)
            { OnLevelLose(); }
        };
        BugBitsChange = (amount) => { bugBits += amount; OnUIChange?.Invoke(); };

        if (SceneManager.GetActiveScene().name == "London1")
        {
            currentlyPlayingLevel = 1;
        }
        if (SceneManager.GetActiveScene().name == "London2")
        {
            currentlyPlayingLevel = 2;
        }
        if (SceneManager.GetActiveScene().name == "Cairo1")
        {
            currentlyPlayingLevel = 3;
        }
        if (SceneManager.GetActiveScene().name == "Cairo2")
        {
            currentlyPlayingLevel = 4;
        }
        if (SceneManager.GetActiveScene().name == "Kyoto1")
        {
            currentlyPlayingLevel = 5;
        }
        if (SceneManager.GetActiveScene().name == "Rio1")
        {
            currentlyPlayingLevel = 6;
        }
        saving = transform.Find("JSONSave").GetComponent<JSONSaving>();
    }

    public void OnLevelCompleted()
    {
        _levelEnd.SetActive(true);
        LevelCompletion.Instance.Victory(GetAmountOfStars());

        if (currentlyPlayingLevel == playerData.level)
        {
            int[] saveStar = new int[6];
            for (int i = 0; i < saveStar.Length; i++)
            {
                if (i == (playerData.level - 1))
                {
                    saveStar[i] = GetAmountOfStars();
                }
                else
                {
                    saveStar[i] = playerData.starsEachLevel[i];
                }
            }
            playerData = new PlayerData(playerData.level + 1, saveStar, playerData.stars + GetAmountOfStars());
            
            saving.SaveData();
        }
        else if (currentlyPlayingLevel < playerData.level)
        {
            int[] saveStar = new int[6];
            int adding = 0;
            for (int i = 0; i < saveStar.Length; i++)
            {
                if (i == (currentlyPlayingLevel - 1) && playerData.starsEachLevel[currentlyPlayingLevel-1] < GetAmountOfStars())
                {
                    saveStar[i] = GetAmountOfStars();
                }
                else
                {
                    saveStar[i] = playerData.starsEachLevel[i];
                }
                adding += saveStar[i];
            }

            playerData = new PlayerData(playerData.level, saveStar, adding);
            
            saving.SaveData();
        }
    }

    private int GetAmountOfStars()
    {
        if (health > 9)
        {
            return 3;
        }
        else if (health >= 6)
        {
            return 2;
        }
        else
        {
            return 1;
        }        
    }

    public void OnLevelLose()
    {
        LevelCompletion.Instance.GameOver();
    }

}
