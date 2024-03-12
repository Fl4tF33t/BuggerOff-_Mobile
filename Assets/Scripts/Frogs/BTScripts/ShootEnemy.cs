using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEditor;

public class ShootEnemy : ActionNode
{
    bool isFinishedAnim;

    protected override void OnStart() {
        //here we trigger the event to attack a target
        //have the tree subscribe to an event and only have it suceed when the event is finished
        context.frogBrain.OnTriggerEvent("hello");
        context.animator.SetTrigger("OnAttack");
        context.animationEvents.OnEndAnim += AnimationEvents_OnEndAnim;
    }

    private void AnimationEvents_OnEndAnim()
    {
        IBugTakeDamage bugDamage = blackboard.selectedTarget.GetComponent<IBugTakeDamage>();
        bugDamage.BugTakeDamage(context.frogBrain.frog.damage);
        isFinishedAnim = true;
    }

    protected override void OnStop() {
        context.animationEvents.OnEndAnim -= AnimationEvents_OnEndAnim;
        isFinishedAnim = false;
    }

    protected override State OnUpdate() {
        if (isFinishedAnim)
        {
            return State.Success;
        }
        return State.Running;
    }
}
