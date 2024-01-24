using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class TimerCountdown : ActionNode
{
    protected override void OnStart() {
        if (blackboard.timer <= 0 && blackboard.resetTimer)
        {
            blackboard.resetTimer = false;
            blackboard.timer = 10;
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        blackboard.timer -= Time.deltaTime;
        return State.Success;
    }
}
