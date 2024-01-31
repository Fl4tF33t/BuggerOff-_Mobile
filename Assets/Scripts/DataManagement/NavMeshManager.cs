using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using Unity.VisualScripting;

public class NavMeshManager : MonoBehaviour
{
    //here you can put mopre than a single navmesh surface if you want to have multiple layers. for example, different bugs on the same path
    public NavMeshSurface flyBugNavMeshSurface;
    public NavMeshSurface groundBugNavMeshSurface;

    //the path that is created for the bugs to follow
    public GameObject road;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = road.GetComponent<PolygonCollider2D>().CreateMesh(true, true);
        GameObject roadRenderMeshes = new GameObject("RoadRenderMeshes");
        InitializeRoad(roadRenderMeshes, mesh);
        groundBugNavMeshSurface.BuildNavMesh();
        groundBugNavMeshSurface.transform.position = new Vector3(0, 0, 0);
        flyBugNavMeshSurface.BuildNavMesh();
        flyBugNavMeshSurface.transform.position = new Vector3(0, 5, 0);
    } 

    void InitializeRoad(GameObject obj, Mesh mesh)
    {
        obj.transform.SetParent(road.transform);
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        obj.GetComponent<MeshFilter>().mesh = mesh;
        road.transform.rotation = Quaternion.Euler(90, 0, 0);
    }


}
