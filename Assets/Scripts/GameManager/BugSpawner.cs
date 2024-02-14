using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script spawns the wave of bugs on a time interval
//It listens to when the GameManager signals off that there is need to spawn something
public class BugSpawner : MonoBehaviour
{
    private Transform spawnPoint;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    [Range(min: 0.1f, max: 1f)] // clamp Step to some reasonable values
    public float Step = 1f;
    public List<Vector2> spawnablePositions;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnSpawnBugs += Instance_OnSpawnBugs;

        spawnPoint = transform.Find("SpawnPoint");
        minX = spawnPoint.position.x - .75f;
        maxX = spawnPoint.position.x + .75f;
        minY = spawnPoint.position.y - .75f;
        maxY = spawnPoint.position.y + .75f;
        for (float x = minX; x < maxX; x += Step)
        {
            for (float y = minY; y < maxY; y += Step)
            {
                spawnablePositions.Add(new Vector2(x, y));
                if (spawnablePositions.Count > 20)
                {
                    return;
                }
            }
        }
    }

    private void Instance_OnSpawnBugs(object sender, GameManager.OnSpawnBugsEvenetArgs e)
    {
        int i;
        for (i = 0; i < e.amount; i++)
        {
            GameObject bug = ObjectPool.Instance.GetPooledObject(e.bugType);
            if (bug != null)
            {
                if(e.bugType == CSVReader.BugType.Centipede || e.bugType == CSVReader.BugType.GiantCentipede)
                {
                    bug.transform.position = Vector3.zero;
                    bug.transform.rotation = Quaternion.identity;
                    bug.SetActive(true);
                }
                else
                bug.transform.position = spawnablePositions[i];
                bug.transform.rotation = Quaternion.identity;
                bug.SetActive(true);
            } 
        }
        if(e.amount2 != 0)
        {
            for (int j = 0; j < e.amount2; j++)
            {
                GameObject bug = ObjectPool.Instance.GetPooledObject(e.bugType2);
                if (bug != null)
                {
                    if (e.bugType2 == CSVReader.BugType.Centipede || e.bugType == CSVReader.BugType.GiantCentipede)
                    {
                        bug.transform.position = Vector3.zero;
                        bug.transform.rotation = Quaternion.identity;
                        bug.SetActive(true);
                    }
                    else
                    bug.transform.position = spawnablePositions[i + j + 1];
                    bug.transform.rotation = Quaternion.identity;
                    bug.SetActive(true);
                }
            }
        }
    }

}
