using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Jump : ActionNode
{
    public float rotationThreshold = 1.0f; // The threshold angle for considering the rotation as correct
    private bool isRotationCorrect;

    public float distanceThreshold = 0.05f;
    public float moveSpeed = 2f;

    private bool isJumpAnimStart;
    protected override void OnStart() {
        context.agent.enabled = false;

        isJumpAnimStart = false;
        isRotationCorrect = false;
    }

    protected override void OnStop() {
        context.agent.enabled = true;
    }

    private void Track(Vector3 position)
    {
        Vector3 targetDirection = position - context.transform.position;
        targetDirection.y = 0; // Restrict rotation to the XZ plane

        // Calculate the desired rotation based on the direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Apply the rotation only on the Y-axis
        context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, targetRotation, rotationThreshold);

        // Check if the rotation is correct
        float angleDifference = Quaternion.Angle(context.transform.rotation, targetRotation);
        isRotationCorrect = (angleDifference <= rotationThreshold);
    }

    protected override State OnUpdate() {
        // Calculate the direction to the target
        if(!isRotationCorrect)
        {
            Track(blackboard.jumpLocation);
        }

        if(!isJumpAnimStart && isRotationCorrect)
        {
            context.animator.SetBool("OnJump", true);
            isJumpAnimStart = true;
        }

        if (isRotationCorrect)
        {
            Vector3 direction = (blackboard.jumpLocation - context.transform.position).normalized;
            float distanceToTarget = Vector3.Distance(context.transform.position, blackboard.jumpLocation);

            // If the object is far from the target, move towards it
            if (distanceToTarget > distanceThreshold)
            {
                context.transform.position += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                // If the object is close enough to the target, consider it reached
                context.animator.SetBool("OnJump", false);
                return State.Success;
            }
        }

        return State.Running;

        
    }
}
