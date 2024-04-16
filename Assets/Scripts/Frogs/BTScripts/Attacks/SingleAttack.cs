using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class SingleAttack : ActionNode
{
    private bool endAnim;
    private float tongueDistance = 9f;
    private float tongueSpeed = 100f;

    protected override void OnStart() {
        endAnim = false;
        if (context.frogBrain.attackType == LogicSO.AttackType.Single)
        {
            Vector3 direction = blackboard.selectedTarget.transform.position - context.transform.position;
            float distance = direction.magnitude;
            context.frogBrain.StartCoroutine(Toungue(distance * tongueDistance, tongueSpeed));
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.frogBrain.attackType != LogicSO.AttackType.Single)
        {
            return State.Failure;
        }
        if (blackboard.selectedTarget == null)
        {
            return State.Failure;
        }
        if (endAnim)
        {
            return State.Success;
        }

        return State.Running;
    }

    private IEnumerator Toungue(float maxScale, float scaleSpeed)
    {
        while (maxScale > context.frogBrain.singleAttack.localScale.y && blackboard.selectedTarget != null)
        {
            context.frogBrain.singleAttack.localScale += Vector3.up * Time.deltaTime * scaleSpeed;
            yield return null;
        }
        context.frogBrain.StartCoroutine(Shrink(scaleSpeed));
    }

    private IEnumerator Shrink(float scaleSpeed)
    {
        while (context.frogBrain.singleAttack.localScale.y >= .99)
        {
            context.frogBrain.singleAttack.localScale += Vector3.down * Time.deltaTime * scaleSpeed;
            yield return null;
        }
        if(blackboard.selectedTarget != null || blackboard.selectedTarget.activeSelf)
        {
            if(context.frogBrain.isBuffed)
            {
                int damage = context.frogBrain.frog.damage + context.frogBrain.buffValue.damage;
                blackboard.selectedTarget.GetComponent<IBugTakeDamage>().BugTakeDamage(damage);
            }else
            blackboard.selectedTarget.GetComponent<IBugTakeDamage>().BugTakeDamage(context.frogBrain.frog.damage);
        }
        endAnim = true;
    }
}
