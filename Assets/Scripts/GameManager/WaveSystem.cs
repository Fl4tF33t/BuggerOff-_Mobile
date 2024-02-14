using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : Singleton<WaveSystem>
{
    public int currentWave;
    public int waveLimit;
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
        if(index == 0)
        {
            timer = CSVReader.Instance.nodeDataArray[index].time;
        }else if (index != 0)
        {
            timer = CSVReader.Instance.nodeDataArray[index].time - CSVReader.Instance.nodeDataArray[index - 1].time;
        }
        yield return new WaitForSeconds(timer);
        Debug.Log("Testing");
        index++;
        if(index < CSVReader.Instance.nodeDataArray.Length)
        {
            StartCoroutine(Waves());
        }
        else if(index == CSVReader.Instance.nodeDataArray.Length)
        {
            if (currentWave < CSVReader.Instance.csvFile.Length)
            {
                ResetNewWave(); 
            }
        }
    }

    void ResetNewWave()
    {
        currentWave++;
        CSVReader.Instance.ReadCSV(currentWave);
        index = 0;
    }
}
