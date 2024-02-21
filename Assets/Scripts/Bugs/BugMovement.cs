using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugMovement : MonoBehaviour
{ 
    // Destination the agent should reach
    private BugBrain bugBrain;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    [HideInInspector]
    public int pathIndex;

    private void Awake()
    {     
        bugBrain = GetComponent<BugBrain>();
        agent = GetComponent<NavMeshAgent>();
        InitializeBugMovement(bugBrain.bugSO);
    }

    private void InitializeBugMovement(BugSO bugSO)
    {
        //The agentType and the BaseOffset should be already set in the inspector

        //add all the other movement sutup here (steering)
        agent.speed = bugSO.speed;
        agent.angularSpeed = bugSO.angularSpeed;
        agent.acceleration = bugSO.acceleration;
        agent.stoppingDistance = bugSO.stoppingDistance;
        agent.autoBraking = bugSO.autoBraking;

        agent.avoidancePriority = Random.Range(0, 100);
    }

    private void Start()
    {
        agent.destination = PathManager.Instance.paths[pathIndex].waypoints[currentWaypointIndex].position;  
    }
    
    private void Update()
    {
        // Check if the bug has reached the current waypoint
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // Move to the next waypoint
            SetNextWaypoint();
        }
    }

    private void SetNextWaypoint()
    {
        // Increment the waypoint index
        currentWaypointIndex++;

        // Check if the bug reached the last waypoint
        if (currentWaypointIndex >= PathManager.Instance.paths[pathIndex].waypoints.Count)
        {
            // If so, destroy the bug or trigger game over
            //Destroy(gameObject);
            // You can add game over logic here
            //return;
            currentWaypointIndex = 0;
        }

        // Set the next waypoint as the destination
        agent.SetDestination(PathManager.Instance.paths[pathIndex].waypoints[currentWaypointIndex].position);
    }

}
