using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Idle : ActionNode
{
    protected override void OnStart() {
        context.animator.SetBool("OnIdling", true);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
