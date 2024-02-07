using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckEnemyInArea : ActionNode
{
    protected override void OnStart() {
        //blackboard.colliders = Physics.OverlapSphere(context.transform.position, context.frogBrain.frog.range, context.frogBrain.frogSO.logicSO.targetLayer);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        //used to check if there is an enemy in the area
        //if(blackboard.colliders.Length == 0)
        //{
        //    return State.Failure;
        //}
        //else
        //{
        //    return State.Success;
        //}
        return State.Success;
    }
}
