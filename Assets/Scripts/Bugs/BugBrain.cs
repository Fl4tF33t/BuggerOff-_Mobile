using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BugMovement))]
[RequireComponent(typeof(NavMeshAgent))]
public class BugBrain : MonoBehaviour
{
    public BugSO bugSO;

    private int health;
    private int sheild;

    private void Awake()
    {
        InitializeBugLogic(bugSO);
    }

    private void InitializeBugLogic(BugSO bugSO)
    {
        //set the original values from the SO
        health = bugSO.health;
        sheild = bugSO.sheild;
    }
}
