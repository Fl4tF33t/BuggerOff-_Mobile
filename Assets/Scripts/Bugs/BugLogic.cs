using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugLogic : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
       navMeshAgent.SetDestination(target.position);
    }
}
