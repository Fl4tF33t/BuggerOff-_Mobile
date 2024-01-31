using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
   
    public PolygonCollider2D polygonCollider2D;
    MeshFilter meshFilter;
    public NavMeshSurface navMeshSurface;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = polygonCollider2D.CreateMesh(true, true);
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        navMeshSurface.BuildNavMesh();
    }
}
