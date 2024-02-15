using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : Singleton<WaveSystem>
{
    public event EventHandler OnLevelCompleted;
    public event EventHandler OnWaveCompleted;

    [SerializeField]
    private Transform[] spawnablePositions;

    public int currentWave;
    public int index;
    public float timer;

    private void Start()
    {
        //Initialise the csv file that we are currently reading
        CSVReader.Instance.ReadCSV(currentWave);

        StartCoroutine(Waves());
    }

    public void StartWaveSystem()
    {
        StartCoroutine(Waves());

    }

    IEnumerator Waves()
    {
        //The countdown timer before the event is triggered
        timer = index == 0 ? timer = CSVReader.Instance.nodeDataArray[index].time : CSVReader.Instance.nodeDataArray[index].time - CSVReader.Instance.nodeDataArray[index - 1].time;
        yield return new WaitForSeconds(timer);

        //Here the event is triggered to spawn the enemies
        Debug.Log("Testing");
        SpawnBug(CSVReader.Instance.nodeDataArray[index].bugType, CSVReader.Instance.nodeDataArray[index].amount, CSVReader.Instance.nodeDataArray[index].path);


        //increase the index and see if it is within the array for the next enemy within the wave
        index++;

        //create the recurssive action to keep spawning the bugs during the wave
        if(index < CSVReader.Instance.nodeDataArray.Length)
        {
            StartCoroutine(Waves());
        }

        else if(index == CSVReader.Instance.nodeDataArray.Length)
        {
            currentWave++;
            if (currentWave < CSVReader.Instance.csvFile.Length)
            {
                ResetNewWave();
                StartCoroutine(EndOfWaveCheck());
            }
        }
    }

    void ResetNewWave()
    {
        CSVReader.Instance.ReadCSV(currentWave);
        index = 0;
    }

    void SpawnBug(CSVReader.BugType bugType, int amount, int path)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bug = ObjectPool.Instance.GetPooledObject(bugType);
            if (bug != null)
            {
                bug.transform.position = spawnablePositions[path].position;
                bug.transform.rotation = Quaternion.identity;
                bug.SetActive(true);
            }
        }
    }

    private IEnumerator EndOfWaveCheck()
    {
        while (ObjectPool.Instance.AnyPooledObjectsActiveForAllTypes())
        {
            yield return null;
        }
        OnWaveCompleted?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator EndOfLevelCheck()
    {
        while (ObjectPool.Instance.AnyPooledObjectsActiveForAllTypes())
        {
            yield return null;
        }
        OnLevelCompleted?.Invoke(this, EventArgs.Empty);
    }
}
