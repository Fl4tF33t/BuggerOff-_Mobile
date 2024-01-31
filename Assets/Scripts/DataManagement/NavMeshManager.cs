using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshManager : MonoBehaviour
{
    NavMeshSurface navMeshSurface;
    public GameObject road;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject roadNavMesh = new GameObject("RoadNavMesh");
        roadNavMesh.transform.SetParent(road.transform); 
    }

    
}
