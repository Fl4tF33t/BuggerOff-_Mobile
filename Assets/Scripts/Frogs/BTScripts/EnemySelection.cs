using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class EnemySelection : ActionNode
{
    protected override void OnStart() {
        blackboard.selectedTarget = SelectTarget();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(blackboard.selectedTarget != null)
        {
            return State.Success;
        }
        else return State.Failure;
    }

    private GameObject SelectTarget()
    {
        GameObject target = null;

        switch (context.frogBrain.frog.target)
        {
            case LogicSO.Target.First:
                if (blackboard.collidersInLOS.Count > 0)
                {
                    target = blackboard.collidersInLOS[0].gameObject;
                }
                break;

            case LogicSO.Target.Last:
                int lastIndex = blackboard.collidersInLOS.Count - 1;
                if (lastIndex >= 0)
                {
                    target = blackboard.collidersInLOS[lastIndex].gameObject;
                }
                break;

            case LogicSO.Target.Strongest:
                float maxHealth = float.MinValue;
                foreach (Collider col in blackboard.collidersInLOS)
                {
                    BugBrain bugBrain = col.GetComponent<BugBrain>();
                    if (bugBrain != null && bugBrain.health > maxHealth)
                    {
                        maxHealth = bugBrain.health;
                        target = col.gameObject;
                    }
                }
                break;

            case LogicSO.Target.Weakest:
                float minHealth = float.MaxValue;
                foreach (Collider col in blackboard.collidersInLOS)
                {
                    BugBrain bugBrain = col.GetComponent<BugBrain>();
                    if (bugBrain != null && bugBrain.health < minHealth)
                    {
                        minHealth = bugBrain.health;
                        target = col.gameObject;
                    }
                }
                break;

            case LogicSO.Target.Shield:
                float maxShield = float.MinValue;
                foreach (Collider col in blackboard.collidersInLOS)
                {
                    BugBrain bugBrain = col.GetComponent<BugBrain>();
                    if (bugBrain != null && bugBrain.shield > maxShield)
                    {
                        maxShield = bugBrain.shield;
                        target = col.gameObject;
                    }
                }
                break;

        }

        return target;

    }
}
