using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AOEAttack : ActionNode
{
    private bool endAnim;

    protected override void OnStart() {
        endAnim = false;
        context.animationEvents.OnEndAnim += AnimationEvents_OnEndAnim;

        //here we perform the visual attack, dont have an effect on the bugs until after trigger
        context.animator.SetTrigger("OnAttack");

    }
    
    protected override void OnStop() {
        endAnim = false;
        context.animationEvents.OnEndAnim -= AnimationEvents_OnEndAnim;
    }

    private void AnimationEvents_OnEndAnim()
    {
        //do damage to the effected area
        RaycastHit[] hits = Physics.SphereCastAll(context.transform.position, 0.4f, context.transform.forward, context.frogBrain.frogSO.logicSO.range, LayerMask.GetMask("Bug"));

        // Iterate through the hits array to process each hit
        foreach (RaycastHit hit in hits)
        {
            if(hit.transform.TryGetComponent(out IBugTakeDamage obj))
            {
                obj.BugTakeDamage(context.frogBrain.frog.damage);
            }
        }

        endAnim = true;
    }

    protected override State OnUpdate() {
        //here, keep checking for when the visual is finally done then perform the logic of removing health
        if(!endAnim)
        {
            return State.Running;
        }
        return State.Success;
    }
}
