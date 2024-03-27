using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Jump : ActionNode
{
    protected override void OnStart() {
        context.agent.enabled = false;
    }

    protected override void OnStop() {
        context.agent.enabled = true;
    }

    protected override State OnUpdate() {
        // Calculate the direction to the target
        Vector3 direction = (blackboard.jumpLocation - context.transform.position).normalized;

        // Move towards the target
        context.transform.position += direction * 10f * Time.deltaTime;

        if (context.transform.position == blackboard.jumpLocation)
        {
            return State.Success;
        }
        return State.Running;

        
    }
}
