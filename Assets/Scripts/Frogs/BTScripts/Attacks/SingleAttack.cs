using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;
using UnityEngine.AI;

public class SingleAttack : ActionNode
{
    private bool endAnim;
    private float tongueDistance = 9f;
    private float tongueSpeed = 100f;
    private float mummySpeed = 30f;

    protected override void OnStart() {
        endAnim = false;
        if (context.frogBrain.attackType == LogicSO.AttackType.Single)
        {
            Vector3 direction = blackboard.selectedTarget.transform.position - context.transform.position;
            float distance = direction.magnitude;
            float speed = context.frogBrain.frogSO.logicSO.frogName == "Mummy" ? mummySpeed : tongueSpeed;
            context.frogBrain.StartCoroutine(Toungue(distance * tongueDistance, speed));
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
        
        while (maxScale > context.frogBrain.singleAttack.localScale.y && blackboard.selectedTarget.activeSelf)
        {
            context.frogBrain.singleAttack.localScale += Vector3.up * Time.deltaTime * scaleSpeed;
            yield return null;
        }
        if (context.gameObject.name.Contains("Mummy"))
        {
            blackboard.selectedTarget.GetComponent<BugBrain>().isAttackable = false;
            blackboard.selectedTarget.GetComponent<NavMeshAgent>().enabled = false;
            blackboard.selectedTarget.GetComponent<BugMovement>().enabled = false;

            //context.frogBrain.StartCoroutine(Shrink(scaleSpeed));
        }
        context.frogBrain.StartCoroutine(Shrink(scaleSpeed));
    }

    private IEnumerator Shrink(float scaleSpeed)
    {
        while (context.frogBrain.singleAttack.localScale.y >= .99)
        {
            context.frogBrain.singleAttack.localScale += Vector3.down * Time.deltaTime * scaleSpeed;
            if (context.gameObject.name.Contains("Mummy"))
            {
                blackboard.selectedTarget.transform.position = Vector3.Lerp(blackboard.selectedTarget.transform.position, context.transform.position, scaleSpeed * Time.deltaTime / 10f);
                Debug.Log("pull the fucker");
            }
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

    private IEnumerator MummyShrink(float scaleSpeed)
    {
        while (context.frogBrain.singleAttack.localScale.y >= .99)
        {
            context.frogBrain.singleAttack.localScale += Vector3.down * Time.deltaTime * scaleSpeed;
            // Calculate the new position towards the target
            Vector3 newPosition = Vector3.MoveTowards(blackboard.selectedTarget.transform.position, context.transform.position, scaleSpeed * Time.deltaTime);

            // Move the object to the new position
            blackboard.selectedTarget.transform.position = newPosition;
            yield return null;
        }
        //if (blackboard.selectedTarget != null || blackboard.selectedTarget.activeSelf)
        //{
        //    if (context.frogBrain.isBuffed)
        //    {
        //        int damage = context.frogBrain.frog.damage + context.frogBrain.buffValue.damage;
        //        blackboard.selectedTarget.GetComponent<IBugTakeDamage>().BugTakeDamage(damage);
        //    }
        //    else
        //        blackboard.selectedTarget.GetComponent<IBugTakeDamage>().BugTakeDamage(context.frogBrain.frog.damage);
        //}
        //endAnim = true;
    }
}
