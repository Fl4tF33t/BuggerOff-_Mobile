using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using static FrogBrain;
using UnityEngine.AI;

public class FindJumpLocation : ActionNode
{

    private Vector3 jumpLocation;
    protected override void OnStart() {
        jumpLocation = RandomPointOnCircleEdge(context.transform.position, context.frogBrain.frog.range);

        while (!IsValidLocation(jumpLocation))
        {
            jumpLocation = RandomPointOnCircleEdge(context.transform.position, context.frogBrain.frog.range);
            if (IsValidLocation(jumpLocation))
            {
                blackboard.jumpLocation = jumpLocation;
            }
        }
    }
    protected override void OnStop() {
    }

    private Vector3 RandomPointOnCircleEdge(Vector3 center, float radius)
    {
        // Generate a random angle around the circle in radians
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Calculate the position of the point on the circle edge
        float x = center.x + radius * Mathf.Cos(randomAngle);
        float z = center.z + radius * Mathf.Sin(randomAngle);

        // Set the y-coordinate to match the top-down view
        float y = center.y;

        // Create and return the position vector
        return new Vector3(x, y, z);
    }

    private bool IsValidLocation(Vector3 pos)
    {
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(pos, out navHit, 0.1f, NavMesh.AllAreas))
        {
            //check if there are other game objects that would collide withion the same area
            Collider[] colliders = Physics.OverlapSphere(navHit.position, 0.2f);
            bool res;

            switch (navHit.mask)
            {
                case 1:
                    //ground frogs
                    res = !context.frogBrain.frogSO.logicSO.isWaterFrog && colliders.Length == 0;
                    return res;
                case 8:
                    //path for bugs
                    return false;
                case 16:
                    //water frogs
                    res = context.frogBrain.frogSO.logicSO.isWaterFrog && colliders.Length == 0;
                    return res;
                default:
                    return false;
            }
        }
        return false;
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
