using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AOEAttack : ActionNode
{
    private bool endAnim;

    protected override void OnStart() {
        endAnim = false;

        if (context.frogBrain.attackType == LogicSO.AttackType.AOE)
        {
            context.animationEvents.OnEndAnim += AnimationEvents_OnEndAnim;
            context.animationEvents.OnDamageLogic += AnimationEvents_OnDamageLogic;

            //here we perform the visual attack, dont have an effect on the bugs until after trigger
            context.animator.SetTrigger("OnAttack");
        }
    }

    private void AnimationEvents_OnDamageLogic()
    {
        RaycastHit[] hits = Physics.SphereCastAll(context.transform.position, 0.5f, context.transform.forward, context.frogBrain.frogSO.logicSO.range, LayerMask.GetMask("Bug"));

        // Iterate through the hits array to process each hit
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.TryGetComponent(out IBugTakeDamage obj))
            {
                if (context.gameObject.name.Contains("Desert"))
                {
                    obj.BugSlow();
                }
                if (context.frogBrain.isBuffed)
                {
                    int damage = context.frogBrain.frog.damage + context.frogBrain.buffValue.damage;
                    blackboard.selectedTarget.GetComponent<IBugTakeDamage>().BugTakeDamage(damage);
                }
                else obj.BugTakeDamage(context.frogBrain.frog.damage);
            }
        }
    }

    private void AnimationEvents_OnEndAnim()
    {
        endAnim = true;
    }
    protected override void OnStop()
    {
        endAnim = false;

        if (context.frogBrain.attackType == LogicSO.AttackType.AOE)
        {
            context.animationEvents.OnEndAnim -= AnimationEvents_OnEndAnim;
            context.animationEvents.OnDamageLogic -= AnimationEvents_OnDamageLogic; 
        }
    }

    protected override State OnUpdate() {
        //here, keep checking for when the visual is finally done then perform the logic of removing health
        if(context.frogBrain.attackType != LogicSO.AttackType.AOE)
        {
            return State.Failure;
        }
        if(!endAnim)
        {
            return State.Running;
        }
        return State.Success;
    }
}
