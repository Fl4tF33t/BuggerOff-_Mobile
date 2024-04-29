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
    //[SerializeField] private GameObject _levelEnd;
    

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

        saving = JSONSaving.Instance;
        saving.LoadData();

        bugBits = 9000;

        WaveSystem.Instance.OnLevelCompleted += () => OnLevelCompleted(); 

        HealthChange = (amount) => { health += amount; OnUIChange?.Invoke();
            if (health <= 0)
            { OnLevelLose(); }
        };
        BugBitsChange = (amount) => { bugBits += amount; OnUIChange?.Invoke(); };

        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "London1":
                currentlyPlayingLevel = 1;
                break;
            case "London2":
                currentlyPlayingLevel = 2;
                break;
            case "Cairo1":
                currentlyPlayingLevel = 3;
                break;
            case "Cairo2":
                currentlyPlayingLevel = 4;
                break;
            case "Kyoto1":
                currentlyPlayingLevel = 5;
                break;
            case "Rio1":
                currentlyPlayingLevel = 6;
                break;
            default:
                currentlyPlayingLevel = 0; // Default level or handle if necessary
                break;
        }

    }

    public void OnLevelCompleted()
    {
        Debug.Log("Superdicks");
        //LevelCompletion.Instance.Victory(GetAmountOfStars());

        if (currentlyPlayingLevel == saving.playerData.level)
        {
            int[] saveStar = new int[6];
            for (int i = 0; i < saveStar.Length; i++)
            {
                if (i == (saving.playerData.level - 1))
                {
                    saveStar[i] = GetAmountOfStars();
                    Debug.Log("This dick");
                }
                else
                {
                    saveStar[i] = saving.playerData.starsEachLevel[i];
                    Debug.Log("Not the dick");
                }
                Debug.Log(saveStar[i]);
            }

            playerData = new PlayerData(saving.playerData.level + 1, saveStar, saving.playerData.stars + GetAmountOfStars());
            Debug.Log("dicks are great " + playerData + "        "+ saving.playerData);
            saving.SaveData(playerData);
            saving.LoadData();
            
        }
        else if (currentlyPlayingLevel < saving.playerData.level)
        {
            int[] saveStar = new int[6];
            int adding = 0;
            for (int i = 0; i < saveStar.Length; i++)
            {
                if (i == (currentlyPlayingLevel - 1) && saving.playerData.starsEachLevel[currentlyPlayingLevel-1] < GetAmountOfStars())
                {
                    saveStar[i] = GetAmountOfStars();
                }
                else
                {
                    saveStar[i] = saving.playerData.starsEachLevel[i];
                }
                adding += saveStar[i];
            }

            playerData = new PlayerData(saving.playerData.level, saveStar, adding);
            Debug.Log("dicks are great number 2 " + playerData + "        " + saving.playerData);
            saving.SaveData(playerData);
            saving.LoadData();
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
