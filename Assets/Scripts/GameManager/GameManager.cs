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

    public int Health { get { return health; } }
    public int BugBits { get {  return bugBits; } }

    private void Start()
    {
        bugBits = 9000;

        WaveSystem.Instance.OnLevelCompleted += () => OnLevelCompleted(); 

        HealthChange = (amount) => { health += amount; OnUIChange?.Invoke();
            if (health <= 0)
            { OnLevelLose(); }
        };
        BugBitsChange = (amount) => { bugBits += amount; OnUIChange?.Invoke(); };
    }

    public void OnLevelCompleted()
    {
        _levelEnd.SetActive(true);
        LevelCompletion.Instance.Victory(GetAmountOfStars());
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
