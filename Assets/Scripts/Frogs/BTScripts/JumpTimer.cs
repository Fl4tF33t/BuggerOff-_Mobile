using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System.Runtime.InteropServices.WindowsRuntime;

public class JumpTimer : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.jumpTimer -= Time.deltaTime;
        return State.Success;
    }
}
