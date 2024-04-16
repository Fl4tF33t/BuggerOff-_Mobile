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

        if (context.frogBrain.attackType == LogicSO.AttackType.Projectile)
        {
            context.animationEvents.OnEndAnim += AnimationEvents_OnEndAnim;
            context.animationEvents.OnDamageLogic += AnimationEvents_OnDamageLogic;
            context.animator.SetTrigger("OnAttack"); 
        }
    }

    private void AnimationEvents_OnDamageLogic()
    {
        GameObject prefab = Instantiate(context.frogBrain.projectile, context.frogBrain.projectilePos.position, context.transform.rotation);
        if (context.frogBrain.isBuffed)
        {
            int damage = context.frogBrain.frog.damage + context.frogBrain.buffValue.damage;
            prefab.GetComponent<ProjectileLogic>().damage = damage;
        }
        else prefab.GetComponent<ProjectileLogic>().damage = context.frogBrain.frog.damage;
    }

    private void AnimationEvents_OnEndAnim()
    {
        //shoot projectile
        endAnim = true;
    }
    protected override void OnStop()
    {
        endAnim = false;
        if (context.frogBrain.attackType == LogicSO.AttackType.Projectile)
        {
            context.animationEvents.OnEndAnim -= AnimationEvents_OnEndAnim;
            context.animationEvents.OnDamageLogic -= AnimationEvents_OnDamageLogic;
        }
    }

    protected override State OnUpdate()
    {
        //here, keep checking for when the visual is finally done then perform the logic of removing health
        if(context.frogBrain.attackType != LogicSO.AttackType.Projectile)
        {
            return State.Failure;
        }
        if (!endAnim)
        {
            return State.Running;
        }
        return State.Success;
    }
}
