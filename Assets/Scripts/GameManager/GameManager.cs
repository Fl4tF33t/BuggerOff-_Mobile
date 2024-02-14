using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EndOfWave))]

public class GameManager : Singleton<GameManager>
{
    //private CSVReader csvReader;
    //private ObjectPool objPool;

    public event EventHandler <OnSpawnBugsEvenetArgs> OnSpawnBugs;
    public class OnSpawnBugsEvenetArgs : EventArgs
    {
        public int amount;
        public CSVReader.BugType bugType;
        public int path;
    }

    public event EventHandler OnLevelCompleted;

    public event EventHandler OnWaveCompleted;

    public event EventHandler OnNextWave;

    //private Levels levels;


    public int timer;
    public int wave;
    public int waveLimit;
    public int spawned;
    public int health;
    public int bugBits;

    private void Start()
    {

        waveLimit = CSVReader.Instance.csvFile.Length;
        Time.timeScale = 1;
        health = 10;
        bugBits = 400;
        wave = 0;
        CSVReader.Instance.ReadCSV(wave);

        //StartCoroutine(WaveSystem());
    }

    IEnumerator WaveSystem()
    {
        while (true)
        {

        }
    }

    private void WaveSpawnTimer()
    {
        timer++;
        for (int i = 0; i < CSVReader.Instance.nodeDataArray.Length; i++)
        {
            if(timer == CSVReader.Instance.nodeDataArray[i].time)
            {
                OnSpawnBugs?.Invoke(this, new OnSpawnBugsEvenetArgs
                {
                    amount = CSVReader.Instance.nodeDataArray[i].amount,
                    bugType = CSVReader.Instance.nodeDataArray[i].bugType,
                });
                spawned++;

                if (spawned == CSVReader.Instance.tableSize)
                {
                    CancelInvoke();
                    if (wave < waveLimit - 1)
                    {
                        wave++;
                        //levels.nextWave.gameObject.SetActive(true);
                        timer = 0;
                        spawned = 0;
                        CSVReader.Instance.ReadCSV(wave);
                        StartCoroutine("EndOfWaveCheck");
                    }
                    else if (wave ==  waveLimit - 1)
                    {
                        StartCoroutine("EndOfLevelCheck");
                    }   
                } 

            }

        }
    }

    public void NextWave()
    {
        OnNextWave?.Invoke(this, EventArgs.Empty);
        InvokeRepeating("WaveSpawnTimer", .1f, 1);
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
