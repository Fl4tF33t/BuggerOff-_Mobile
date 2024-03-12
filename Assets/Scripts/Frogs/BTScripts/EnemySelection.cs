using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class EnemySelection : ActionNode
{
    protected override void OnStart() {
        SelectTarget();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(blackboard.selectedTarget != null)
        {
            return State.Success;
        }
        else return State.Failure;
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
