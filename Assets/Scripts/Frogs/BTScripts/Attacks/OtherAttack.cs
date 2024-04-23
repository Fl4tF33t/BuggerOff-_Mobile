using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class OtherAttack : ActionNode
{
    private bool endAnim;

    protected override void OnStart() {

        endAnim = false;

        float dist = Vector3.Distance(context.transform.position, blackboard.selectedTarget.transform.position);
        float closeRange = context.frogBrain.frog.range / 100f * 75f;

        if (context.frogBrain.attackType == LogicSO.AttackType.Other)
        {
            
            switch (dist)
            {
                case float when dist < closeRange:
                    context.animationEvents.OnEndAnim += AnimationEvents_OnEndAnimClose;
                    context.animationEvents.OnDamageLogic += AnimationEvents_OnDamageLogic;
                    context.animator.SetTrigger("OnCloseCombat");
                    break;
                case float when dist >= closeRange:
                    context.animationEvents.OnEndAnim += AnimationEvents_OnEndAnimRange;
                    context.animationEvents.OnDamageLogic += AnimationEvents_OnDamageLogic1;
                    context.animator.SetTrigger("OnRangeAttack");
                    break;
                default:
                    // Handle any other cases
                    break;
            } 
        }
    }

    private void AnimationEvents_OnDamageLogic1()
    {
        GameObject prefab = Instantiate(context.frogBrain.projectile, context.frogBrain.projectilePos.position, context.transform.rotation);
        prefab.GetComponent<ProjectileLogic>().damage = context.frogBrain.frog.damage;
    }

    private void AnimationEvents_OnDamageLogic()
    {
        if (blackboard.selectedTarget != null || blackboard.selectedTarget.activeSelf)
        {
            blackboard.selectedTarget.GetComponent<IBugTakeDamage>().BugTakeDamage(context.frogBrain.frog.damage);
        }
    }

    private void AnimationEvents_OnEndAnimClose()
    {
        endAnim = true;
    }
    private void AnimationEvents_OnEndAnimRange()
    {
        
        
        endAnim = true;
    }

    protected override void OnStop() {
        if (context.frogBrain.attackType == LogicSO.AttackType.Other)
        {
            context.animationEvents.OnEndAnim -= AnimationEvents_OnEndAnimRange;
            context.animationEvents.OnEndAnim -= AnimationEvents_OnEndAnimClose;
            context.animationEvents.OnDamageLogic -= AnimationEvents_OnDamageLogic;
            context.animationEvents.OnDamageLogic -= AnimationEvents_OnDamageLogic1;

        }
    }

    protected override State OnUpdate() {
        if (context.frogBrain.attackType != LogicSO.AttackType.Other)
        {
            return State.Failure;
        }
        if(endAnim)
        {
            return State.Success;
        }
        return State.Running;
    }
}
