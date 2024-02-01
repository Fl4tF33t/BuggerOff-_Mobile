using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class TrackEnemy : ActionNode
{
    float timer = 5;
    public float rotationThreshold = 1.0f; // The threshold angle for considering the rotation as correct
    private bool isRotationCorrect = false;

    protected override void OnStart() {
        timer = 5;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        timer -= Time.deltaTime;
        Vector3 targetDirection = blackboard.colliders[0].transform.position - context.transform.position;
        targetDirection.y = 0; // Restrict rotation to the XZ plane

        // Calculate the desired rotation based on the direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Apply the rotation only on the Y-axis
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, targetRotation, rotationThreshold);

        // Check if the rotation is correct
        float angleDifference = Quaternion.Angle(context.transform.rotation, targetRotation);
        isRotationCorrect = (angleDifference <= rotationThreshold);
        if (timer < 0)
        {
            return State.Success;
        }
        return State.Running;
    }
}
