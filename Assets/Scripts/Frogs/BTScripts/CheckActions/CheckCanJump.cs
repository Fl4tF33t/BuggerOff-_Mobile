using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckCanJump : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(blackboard.timer <= 0)
        {
            blackboard.timer = 10;
            return State.Success;
        }
        return State.Failure;

    }
}
