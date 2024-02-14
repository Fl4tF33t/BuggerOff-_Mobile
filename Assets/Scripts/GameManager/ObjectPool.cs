using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to to create a pool of objects, having them ready to be used
//Array of objects is created and filled with the prefabs that are set in the inspector
//The objects will be set to inactive and will be activated from another script
public class ObjectPool : MonoBehaviour
{
    private Transform location;
    public static ObjectPool SharedInstance;

    [Serializable]
    public class PooledObjects
    {
        public List<GameObject> pooledObjects = new List<GameObject>();
    }
    public PooledObjects[] separatedPooledObjects;

    [Serializable]
    public struct ObjectsToPool
    {
        public GameObject prefab;
        public int amount; 
    }
    public ObjectsToPool[] objectsToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        location = GameObject.Find("SpawnPoint").transform;

        // Initialize the separate object pools based on defined objects and amounts
        separatedPooledObjects = new PooledObjects[objectsToPool.Length];
        GameObject tmp;
        for (int typesOfObjects = 0; typesOfObjects < objectsToPool.Length; typesOfObjects++)
        {
            separatedPooledObjects[typesOfObjects] = new PooledObjects();
            for (int amountOfObjects = 0; amountOfObjects < objectsToPool[typesOfObjects].amount; amountOfObjects++)
            {
                // Instantiate objects and add them to the pool
                tmp = Instantiate(objectsToPool[typesOfObjects].prefab, location.position, Quaternion.identity);
                tmp.SetActive(false);
                separatedPooledObjects[typesOfObjects].pooledObjects.Add(tmp);
            }
        }
    }

    // Retrieve an inactive object from the pool based on bug type
    public GameObject GetPooledObject(CSVReader.BugType type)
    {
        for (int i = 0; i < separatedPooledObjects[(int)type].pooledObjects.Count; i++)
        {
            if (!separatedPooledObjects[(int)type].pooledObjects[i].activeInHierarchy)
            {
                return separatedPooledObjects[(int)type].pooledObjects[i];
            }
        }
        return null;
    }

    // Check if any objects are currently active in the pool for a specific bug type
    public bool AnyPooledObjectsActive(CSVReader.BugType type)
    {
        for (int i = 0; i < separatedPooledObjects[(int)type].pooledObjects.Count; i++)
        {
            if (separatedPooledObjects[(int)type].pooledObjects[i].activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    // Check if any objects are currently active in the pool for all bug types
    public bool AnyPooledObjectsActiveForAllTypes()
    {
        foreach (CSVReader.BugType type in Enum.GetValues(typeof(CSVReader.BugType)))
        {
            if (AnyPooledObjectsActive(type))
            {
                return true; 
            }
        }
        return false; 
    }
}
