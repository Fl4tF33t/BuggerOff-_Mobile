using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bug", menuName = "ScriptableObjects/Bug", order = 1)]
public class BugSO : ScriptableObject
{
    public string bugName;
    public int health;
    public int sheild;
    public int moneyDrop;
    public int damageToPlayer;

    //All these variable are for the bug's movement with the use of NavMeshAgent
    public float speed;
    public float angularSpeed;
    public float acceleration;
    public float stoppingDistance;
    public bool autoBraking;
}
