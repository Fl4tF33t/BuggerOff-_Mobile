using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.UIElements;

public class BuffFrog : ActionNode
{
    List<Collider> oldColliderList = new List<Collider>();
    List<Collider> newColliderList = new List<Collider>();



    protected override void OnStart() {


        foreach (var collider in blackboard.collidersInArea)
        {
            if (!collider.gameObject.name.Contains("Sunglasses"))
            {
                if (collider.TryGetComponent(out FrogBrain frog))
                {
                    if (!oldColliderList.Contains(collider))
                    {
                        oldColliderList.Add(collider);
                    }
                }
            }
        }

        newColliderList.Clear();

        foreach (var collider in blackboard.collidersInArea)
        {
            if (!collider.gameObject.name.Contains("Sunglasses"))
            {
                if (collider.TryGetComponent(out FrogBrain frog))
                {
                    newColliderList.Add(collider);
                }
            }
        }


        for (int i = 0; i < oldColliderList.Count; i++)
        {
            var collider = oldColliderList[i];
            if (!newColliderList.Contains(collider))
            {
                collider.GetComponent<FrogBrain>().isBuffed = false;
                oldColliderList.RemoveAt(i);
            }
        }
        

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {


        if (newColliderList.Count > 0)
        {
            foreach (var collider in newColliderList)
            {
                FrogBrain frog = collider.GetComponent<FrogBrain>();
                frog.isBuffed = true;
                frog.buffValue.damage = context.frogBrain.frogSO.logicSO.damage;
                frog.buffValue.range = context.frogBrain.frogSO.logicSO.range;
                frog.buffValue.attackSpeed = context.frogBrain.frogSO.logicSO.attackSpeed;

            }
            if (!context.animator.GetCurrentAnimatorStateInfo(0).IsName("OnTracking"))
            {
                context.animator.SetTrigger("OnTracking");
            }
            return State.Success;
        }
        return State.Failure;
    }
}
