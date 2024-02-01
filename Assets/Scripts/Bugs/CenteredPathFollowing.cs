using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CenteredPathFollowing : MonoBehaviour
{
    public Transform target; // Destination the agent should reach
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //CalculateAndFollowCenteredPath();
    }

    void CalculateAndFollowCenteredPath()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(target.position, path);

        // Calculate centered path
        Vector3[] centeredPath = CalculateCenteredPath(path);

        // Follow the centered path
        StartCoroutine(FollowPath(centeredPath));
    }

    Vector3[] CalculateCenteredPath(NavMeshPath path)
    {
        // Adjust waypoints to be in the center of the path
        // This is a simplified example, more complex logic may be needed
        Vector3[] centeredPath = new Vector3[path.corners.Length];
        for (int i = 0; i < path.corners.Length; i++)
        {
            centeredPath[i] = path.corners[i]; // Adjust as needed
        }
        return centeredPath;
    }

    IEnumerator FollowPath(Vector3[] path)
    {
        foreach (Vector3 waypoint in path)
        {
            agent.SetDestination(waypoint);
            yield return new WaitForFixedUpdate(); // Adjust as needed
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateAndFollowCenteredPath();
        }
    }
}
