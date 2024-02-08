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
        //here I need to implement the code that dictates which target the frog is following
        SelectTarget();

        //the timer should be set to the time it takes to shoot a target
        timer = .5f;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        timer -= Time.deltaTime;
        if (blackboard.selectedTarget != null)
        {
            Vector3 targetDirection = blackboard.selectedTarget.transform.position - context.transform.position;
            targetDirection.y = 0; // Restrict rotation to the XZ plane

            // Calculate the desired rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Apply the rotation only on the Y-axis
            context.transform.rotation = Quaternion.RotateTowards(context.transform.rotation, targetRotation, rotationThreshold);

            // Check if the rotation is correct
            float angleDifference = Quaternion.Angle(context.transform.rotation, targetRotation);
            isRotationCorrect = (angleDifference <= rotationThreshold); 
        }

        if(!blackboard.selectedTarget.activeSelf)
        {
            if (blackboard.collidersInLOS.Count == 0)
            {
                return State.Failure;
            }else SelectTarget();
        }

        if (timer < 0)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void SelectTarget()
    {
        switch (context.frogBrain.frog.target)
        {
            case FrogBrain.Target.First:
                blackboard.selectedTarget = blackboard.collidersInLOS[0].gameObject;
                break;
            case FrogBrain.Target.Last:
                blackboard.selectedTarget = blackboard.collidersInLOS[blackboard.collidersInLOS.Count - 1].gameObject;
                break;
            case FrogBrain.Target.Strongest:
                //foreach loop to see which has the highest health
                break;
            case FrogBrain.Target.Weakest:
                //foreach loop to see which has the lowest health
                break;
        }
    }
}
