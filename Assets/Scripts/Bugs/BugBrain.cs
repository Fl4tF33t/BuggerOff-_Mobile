using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BugMovement))]
[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Rigidbody))]

public class BugBrain : MonoBehaviour, ITakeDamage
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

    public void TakeDamage(int damage)
    {
        //check if the bug has sheild
        if (sheild > 0)
        {
            //if so, reduce the sheild
            sheild -= damage;
            //if the sheild is less than 0, set it to 0
            if (sheild < 0)
                sheild = 0;
        }
        else
        {
            //if not, reduce the health
            health -= damage;
            //if the health is less than 0, set it to 0
            if (health < 0)
                health = 0;
        }

        //check if the bug is dead
        if (health <= 0)
        {
            //if so, destroy the bug
            Destroy(gameObject);
        }
    }   
}
