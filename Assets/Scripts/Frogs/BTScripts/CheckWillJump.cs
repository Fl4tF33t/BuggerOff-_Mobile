using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckWillJump : ActionNode
{
    [Range(0, 10)]
    public float chanceOfJump;
    private float randomNum;

    protected override void OnStart() {
        randomNum = Random.Range(0, 10);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(randomNum <= chanceOfJump)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
