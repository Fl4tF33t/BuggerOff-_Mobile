using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckJumpTimer : ActionNode
{
    public int randomNum;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
        if (blackboard.jumpTimer <= 0)
        {
            blackboard.jumpTimer = 10f;
        }
    }

    protected override State OnUpdate()
    {
        Debug.Log("Jump 3");

        randomNum = Random.Range(0, 6);
      
        if (randomNum > context.frogBrain.frog.discipline)
        {
            return State.Success;
        }
        return State.Failure;
    }
}
