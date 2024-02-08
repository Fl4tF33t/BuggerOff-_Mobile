using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class CheckEnemy : ActionNode
{
    private Collider[] collidersInRange;
    protected override void OnStart() {
        blackboard.collidersInLOS.Clear();

        //Find the Targets in the designated range and determine which are in line of sight, add those to an accessible list
        blackboard.col = Physics.OverlapSphere(context.transform.position, context.frogBrain.frog.range, context.frogBrain.frogSO.logicSO.targetLayer);
        foreach (Collider target in blackboard.col)
        {
            //LOS is also blocked by other bugs and should be considered when calculating
            //Ray ray = new Ray(context.transform.position, target.transform.position - context.transform.position);
            //RaycastHit hit;
            //if(Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Frog")))
            //{
            //    if(hit.collider == target)
            //    {
            //        blackboard.collidersInLOS.Add(target);
            //    }
            //}
            blackboard.collidersInLOS.Add(target);
            Ray ray = new Ray(context.transform.position, target.transform.position - context.transform.position);
            float distance = Vector3.Distance(context.transform.position, target.transform.position);
            RaycastHit[] hits = Physics.RaycastAll(ray, distance, ~LayerMask.GetMask("Frog"));

            foreach (RaycastHit hit in hits)
            {
                //if (hit.collider.gameObject.layer != LayerMask.GetMask("Bug"))
                //{
                //    blackboard.collidersInLOS.Remove(target);
                //    break;
                //}
                if (hit.collider.gameObject.layer == LayerMask.GetMask("Bug")) continue;
                else
                {
                    blackboard.collidersInLOS.Remove(target);
                    break;
                }
            }
        }
        
    }

    protected override void OnStop() {
        //Array.Clear(collidersInRange, 0, blackboard.col.Length);
    }

    protected override State OnUpdate() {
        State result = blackboard.col.Length == 0 || blackboard.collidersInLOS.Count == 0 ? State.Failure : State.Success;
        return result;
    }
}
