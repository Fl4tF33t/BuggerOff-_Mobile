using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class OtherAttack : ActionNode
{
    protected override void OnStart() {

        float dist = Vector3.Distance(context.transform.position, blackboard.selectedTarget.transform.position);
        float closeRange = context.frogBrain.frog.range / 100f * 75f;

        if (context.frogBrain.attackType == LogicSO.AttackType.Other)
        {
            switch (dist)
            {
                case float when closeRange <= dist:
                    // Handle distances less than closeRange
                    break;
                case float when closeRange >= dist:
                    // Handle distances greater than closeRange
                    break;
                default:
                    // Handle any other cases
                    break;
            } 
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.frogBrain.attackType != LogicSO.AttackType.Other)
        {
            return State.Failure;
        }
        return State.Success;
    }
}
