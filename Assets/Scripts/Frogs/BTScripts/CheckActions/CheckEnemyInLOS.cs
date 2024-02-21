using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckEnemyInLOS : ActionNode
{
    protected override void OnStart() {
       
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        RaycastHit hit;
        foreach (Collider item in blackboard.collidersInArea)
        {
            if (Physics.Raycast(context.transform.position, item.transform.position - context.transform.position, out hit))
            {
                if (hit.collider == item)
                {
                    blackboard.collidersInLOS.Add(item);
                    return State.Success;
                }
                else continue;
            }
            else
            {
                return State.Failure;
            }
        }
        return State.Failure;
    }
}
