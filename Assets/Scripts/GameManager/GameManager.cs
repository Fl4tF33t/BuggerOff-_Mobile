using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int bugBits;

    //Santiago
    //[SerializeField] private GameObject _levelEnd;


    public Action<int> HealthChange;
    public Action<int> BugBitsChange;

    public event Action OnUIChange;

    private PlayerData playerData;

    public int Health { get { return health; } }
    public int BugBits { get { return bugBits; } }

    public int currentlyPlayingLevel;
    private string sceneName;
    private JSONSaving saving;

    private void Start()
    {
        Time.timeScale = 1;
        saving = JSONSaving.Instance;
        if (saving != null)
        {
            playerData = saving.PlayerData;

        }
        sceneName = SceneManager.GetActiveScene().name;

        bugBits = 500;

        WaveSystem.Instance.OnLevelCompleted += () => OnLevelCompleted();

        HealthChange = (amount) => {
            health += amount; OnUIChange?.Invoke();
            if (health <= 0)
            { OnLevelLose(); }
        };
        BugBitsChange = (amount) => { bugBits += amount; OnUIChange?.Invoke(); };
    }

    public void OnLevelCompleted()
    {
        LevelCompletion.Instance.Victory(GetAmountOfStars());

        foreach (var item in playerData.cityList)
        {
            if (item.cityName == sceneName)
            {
                item.isCompleted = true;

                if (GetAmountOfStars() >= item.numberOfStars)
                {
                    item.numberOfStars = GetAmountOfStars();
                }

                saving.PlayerData = playerData;
                break;
            }
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
