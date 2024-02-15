using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    //Singleton, should only be one path manager in the scene

    //This is a list of all the waypoints in the scene, have them public for the ai to reference
    private Transform[] transformArray;
    [HideInInspector]
    public List<Transform> waypointsPath1 = new List<Transform>();

    [HideInInspector]
    public List<Transform> waypoints = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();
        transformArray = GetComponentsInChildren<Transform>();
        foreach (Transform transform in transformArray)
        {
            if (transform.tag == "Waypoint")
                waypoints.Add(transform);
        }
    }
}
