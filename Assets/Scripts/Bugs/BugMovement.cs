using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class BugMovement : MonoBehaviour
{ 
    // Destination the agent should reach
    private BugSO bugSO;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    //[HideInInspector]
    public int pathIndex;

    private void Awake()
    {     
        if(TryGetComponent(out BugBrain bugB))
        {
            bugSO = bugB.bugSO;
            Debug.Log("Found the bug so");
        }
        if(TryGetComponent(out CentipedeBrain centB))
        {
            bugSO = centB.bugSO;
        }

        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        InitializeBugMovement(bugSO);
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }

    private void InitializeBugMovement(BugSO bugSO)
    {
        //The agentType and the BaseOffset should be already set in the inspector

        agent.enabled = true;

        //add all the other movement sutup here (steering)
        agent.speed = bugSO.speed;
        agent.angularSpeed = bugSO.angularSpeed;
        agent.acceleration = bugSO.acceleration;
        agent.stoppingDistance = bugSO.stoppingDistance;
        agent.autoBraking = bugSO.autoBraking;
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
            currentWaypointIndex = 0;
        }

        // Set the next waypoint as the destination
        agent.SetDestination(PathManager.Instance.paths[pathIndex].waypoints[currentWaypointIndex].position);
    }

    public IEnumerator SpeedDamage()
    {
        agent.speed *= .5f;
        yield return new WaitForSeconds(1f);
        agent.speed = bugSO.speed;
    }
}
