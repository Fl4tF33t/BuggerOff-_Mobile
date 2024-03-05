using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckEnemyInLOS : ActionNode
{
    protected override void OnStart() {
       
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        //RaycastHit hit;
        //foreach (Collider item in blackboard.collidersInArea)
        //{
        //    if (Physics.Raycast(context.transform.position, item.transform.position - context.transform.position, out hit))
        //    {
        //        if (hit.collider == item)
        //        {
        //            blackboard.collidersInLOS.Add(item);
        //            //return State.Success;
        //        }
        //        else continue;
        //    }
        //    else
        //    {
        //        return State.Failure;
        //    }
        //}
        //return State.Failure;
        

        blackboard.collidersInLOS.Clear();
        foreach (Collider item in blackboard.collidersInArea)
        {
            if (Physics.Linecast(context.transform.position, item.transform.position, LayerMask.GetMask("BlockLOS")))
            {
                continue;
            }
            else
            {
                blackboard.collidersInLOS.Add(item);
            }
        }
        if(blackboard.collidersInLOS.Count > 0)
        {
            return State.Success;
        }
        else return State.Failure;
    }
}
