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
        collidersInRange = Physics.OverlapSphere(context.transform.position, context.frogBrain.frog.range, context.frogBrain.frogSO.logicSO.targetLayer);
        foreach (Collider target in collidersInRange)
        {
            //LOS is also blocked by other bugs and should be considered when calculating
            Ray ray = new Ray(context.transform.position, target.transform.position - context.transform.position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Frog")))
            {
                if(hit.collider == target)
                {
                    blackboard.collidersInLOS.Add(target);
                }
            }
        }
        
    }

    protected override void OnStop() {
        Array.Clear(collidersInRange, 0, collidersInRange.Length);
    }

    protected override State OnUpdate() {
        State result = collidersInRange.Length == 0 || blackboard.collidersInLOS.Count == 0 ? State.Failure : State.Success;
        return result;
    }
}
