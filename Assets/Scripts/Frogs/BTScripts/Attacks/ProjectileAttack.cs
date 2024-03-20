using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ProjectileAttack : ActionNode
{
    private bool endAnim;

    protected override void OnStart()
    {
        endAnim = false;
        context.animationEvents.OnEndAnim += AnimationEvents_OnEndAnim;

        //here we perform the visual attack, dont have an effect on the bugs until after trigger
        context.animator.SetTrigger("OnAttack");

    }

    protected override void OnStop()
    {
        endAnim = false;
        context.animationEvents.OnEndAnim -= AnimationEvents_OnEndAnim;
    }

    private void AnimationEvents_OnEndAnim()
    {
        //shoot projectile
        context.frogBrain.ShootProjectile();
        Debug.Log("Shoot");
        endAnim = true;
    }

    protected override State OnUpdate()
    {
        //here, keep checking for when the visual is finally done then perform the logic of removing health
        if (!endAnim)
        {
            return State.Running;
        }
        return State.Success;
    }
}
