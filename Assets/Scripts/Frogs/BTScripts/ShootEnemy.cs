using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEditor;

public class ShootEnemy : ActionNode
{
    protected override void OnStart() {
        //here we trigger the event to attack a target
        //have the tree subscribe to an event and only have it suceed when the event is finished
        context.frogBrain.OnTriggerEvent("hello");
        Debug.Log("Hit");
        BugBrain bugBrain = blackboard.selectedTarget.GetComponent<BugBrain>();
        bugBrain.BugTakeDamage(1);
        context.animator.SetTrigger("OnAttack");
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
