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
        Debug.Log("Jump 2");

        blackboard.jumpTimer -= Time.deltaTime;
        if(blackboard.jumpTimer < 0)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
