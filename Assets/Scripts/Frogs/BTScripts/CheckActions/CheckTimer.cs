using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckTimer : ActionNode
{
    public float timer;

    protected override void OnStart() {
        timer = blackboard.attackTimer;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(timer < 0)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
