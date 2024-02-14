using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EndOfWave))]

public class GameManager : Singleton<GameManager>
{
    private CSVReader csvReader;
    private ObjectPool objPool;

    public event EventHandler <OnSpawnBugsEvenetArgs> OnSpawnBugs;
    public class OnSpawnBugsEvenetArgs : EventArgs
    {
        public int amount;
        public int amount2;
        public CSVReader.BugType bugType;
        public CSVReader.BugType bugType2;
    }

    public event EventHandler OnLevelCompleted;

    public event EventHandler OnWaveCompleted;

    public event EventHandler OnNextWave;

    //private Levels levels;


    private int time;
    public int wave;
    [SerializeField]
    private int waveLimit;
    private int spawned;
    static int health;
    static int bugBits = 200;

    public int GetWaveTotal()
    {
        return waveLimit;
    }

    public static int BugBits
    {
        get
        {
            return bugBits;
        }
    }
    public static int Health
    {
        get
        {
            return health;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        csvReader = GetComponent<CSVReader>();
        objPool = GetComponent<ObjectPool>();

    }
    private void Start()
    {
        Time.timeScale = 1;
        health = 10;
        bugBits = 400;
        wave = 0;
        csvReader.ReadCSV(wave);
    }


    private void WaveSpawnTimer()
    {
        time++;
        for (int i = 0; i < csvReader.nodeDataArray.Length; i++)
        {
            if(time == csvReader.nodeDataArray[i].time)
            {
                OnSpawnBugs?.Invoke(this, new OnSpawnBugsEvenetArgs
                {
                    amount = csvReader.nodeDataArray[i].amount,
                    bugType = csvReader.nodeDataArray[i].bugType,
                });
                spawned++;

                if (spawned == csvReader.tableSize)
                {
                    CancelInvoke();
                    if (wave < waveLimit - 1)
                    {
                        wave++;
                        //levels.nextWave.gameObject.SetActive(true);
                        time = 0;
                        spawned = 0;
                        csvReader.ReadCSV(wave);
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
        while (objPool.AnyPooledObjectsActiveForAllTypes())
        {
            yield return null;
        }
        OnLevelCompleted?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator EndOfWaveCheck()
    {
        while (objPool.AnyPooledObjectsActiveForAllTypes())
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
