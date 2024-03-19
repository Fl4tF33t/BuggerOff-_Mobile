using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : Singleton<WaveSystem>
{
    // Event triggered when a level is completed
    public Action OnLevelCompleted;

    // Event triggered when a wave is completed
    public Action OnWaveCompleted;

    [SerializeField]
    private Transform[] spawnablePositions; // Array of spawnable positions for enemies

    [SerializeField]
    private int currentWave;
    private int index; 
    private float timer;

    private void Start()
    {
        // Initialize the wave system for the first wave
        currentWave = 0;
        ResetNewWave();
    }

    // Method to reset parameters for a new wave
    void ResetNewWave()
    {
        CSVReader.Instance.ReadCSV(currentWave); // Read CSV data for the new wave
        index = 0; // Reset index for the new wave
    }

    // Coroutine to handle wave progression
    public IEnumerator Waves()
    {
        // Determine the countdown timer before the event is triggered
        timer = index == 0 ? timer = CSVReader.Instance.nodeDataArray[index].time : CSVReader.Instance.nodeDataArray[index].time - CSVReader.Instance.nodeDataArray[index - 1].time;
        yield return new WaitForSeconds(timer);

        // Trigger event to spawn enemies
        SpawnBug(CSVReader.Instance.nodeDataArray[index].bugType, CSVReader.Instance.nodeDataArray[index].amount, CSVReader.Instance.nodeDataArray[index].path - 1);

        // Move to the next enemy in the wave
        index++;

        // If there are more enemies in the wave, continue spawning
        if (index < CSVReader.Instance.nodeDataArray.Length)
        {
            StartCoroutine(Waves());
        }
        // If all enemies in the wave have been spawned
        else if (index == CSVReader.Instance.nodeDataArray.Length)
        {
            // Move to the next wave if available
            currentWave++;
            if (currentWave < CSVReader.Instance.csvFile.Length)
            {
                ResetNewWave(); // Reset wave parameters for the new wave
                StartCoroutine(EndOfWaveCheck(OnWaveCompleted)); // Check for completion of the wave
            }
            // If all waves in the level have been completed
            else if (currentWave == CSVReader.Instance.csvFile.Length)
            {
                StartCoroutine(EndOfWaveCheck(OnLevelCompleted)); // Check for completion of the level
            }
        }
    }

    // Method to spawn bugs
    void SpawnBug(string bugType, int amount, int path)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bug = ObjectPool.Instance.GetPooledObject(bugType); // Get a bug object from the object pool
            if (bug != null)
            {
                bug.transform.position = spawnablePositions[path].position; // Set bug position
                bug.transform.rotation = Quaternion.identity; // Reset bug rotation
                bug.GetComponent<BugMovement>().pathIndex = path;
                bug.SetActive(true); // Activate the bug
            }
        }
    }

    // Coroutine to check for completion of a wave
    private IEnumerator EndOfWaveCheck(Action completed)
    {
        while (ObjectPool.Instance.AnyPooledObjectsActiveForAllTypes()) // Wait until all enemies from the wave are destroyed
        {
            yield return null;
        }
        completed?.Invoke(); // Trigger wave completed event
    }
}
