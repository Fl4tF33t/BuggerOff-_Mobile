using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Idle : ActionNode
{
    protected override void OnStart() {

        if (!context.animator.GetCurrentAnimatorStateInfo(0).IsName("OnIdling"))
        {
            context.animator.SetTrigger("OnIdling");
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
