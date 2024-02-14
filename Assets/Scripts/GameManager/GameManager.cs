using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public event EventHandler OnLevelCompleted;

    public event EventHandler OnWaveCompleted;

    public event EventHandler OnNextWave;

    //private Levels levels;


    public int spawned;
    public int health;
    public int bugBits;
 
    private void Start()
    {

        health = 10;
        bugBits = 400;

    }

  

    private IEnumerator EndOfLevelCheck()
    {
        while (ObjectPool.Instance.AnyPooledObjectsActiveForAllTypes())
        {
            yield return null;
        }
        OnLevelCompleted?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator EndOfWaveCheck()
    {
        while (ObjectPool.Instance.AnyPooledObjectsActiveForAllTypes())
        {
            yield return null;
        }
        OnWaveCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void HealthChange(int amount)
    {
        health += amount;
    }

    public void BugBitsChange(int amount)
    {
        bugBits += amount;
    }

}
