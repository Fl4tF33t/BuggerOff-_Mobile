using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckEnemyInLOS : ActionNode
{
    protected override void OnStart() {
        blackboard.collidersInLOS.Clear();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        foreach (Collider item in blackboard.collidersInArea)
        {
            if (Physics.Linecast(context.transform.position, item.transform.position, LayerMask.GetMask("BlockLOS")))
            {
                continue;
            }
            else
            {
                blackboard.collidersInLOS.Add(item);
            }
        }
        if(blackboard.collidersInLOS.Count > 0)
        {
            return State.Success;
        }
        else return State.Failure;
    }
}
