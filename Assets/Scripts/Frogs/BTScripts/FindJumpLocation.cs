using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class FindJumpLocation : ActionNode
{
    protected override void OnStart() {
        blackboard.jumpLocation = Vector3.zero;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
