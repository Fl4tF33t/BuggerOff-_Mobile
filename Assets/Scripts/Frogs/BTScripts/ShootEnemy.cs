using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ShootEnemy : ActionNode
{
    protected override void OnStart() {
        //here we trigger the event to attack a target
        //have the tree subscribe to an event and only have it suceed when the event is finished

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
