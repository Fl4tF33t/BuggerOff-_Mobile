using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    //This is a list of all the waypoints in the scene, have them public for the ai to reference
    private Transform[] transformArray;

    [Serializable]
    public class WayPoints
    {
        public List<Transform> waypoints = new List<Transform>();
    }
    public WayPoints[] paths;

    private void Start()
    {        
        //Initialising the list of waypoints
        paths = new WayPoints[transform.GetChild(0).childCount];        
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = new WayPoints();
        }

        //Add the waypoints to the correct list
        transformArray = GetComponentsInChildren<Transform>();
        foreach (Transform transform in transformArray)
        {
            if (transform.tag == "Waypoint1")
            {
                paths[0].waypoints.Add(transform);
            }
            else if (transform.tag == "Waypoint2")
            {
                paths[1].waypoints.Add(transform);
            }
        }
    }
}
