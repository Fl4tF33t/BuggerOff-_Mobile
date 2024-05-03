using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class CentipedeBody : MonoBehaviour, IBugTakeDamage
{
    // Start is called before the first frame update
    NavMeshAgent agent;

    public CentipedeBrain brain;
    public Transform target;
    BugSO bugSO;
    private Coroutine slowDownCoroutine;

    private void Awake()
    {
        bugSO = brain.bugSO;

        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        brain.OnSlow += () => {
            if (this.gameObject.activeSelf)
            {
                if (slowDownCoroutine != null)
                {
                    StopCoroutine(slowDownCoroutine);
                }
                slowDownCoroutine = StartCoroutine(SpeedDamage()); 
            }
        };
    }
    private void OnEnable()
    {
        InitializeBugMovement(bugSO);
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }

    private void InitializeBugMovement(BugSO bugSO)
    {
        //The agentType and the BaseOffset should be already set in the inspector

        agent.enabled = true;

        //add all the other movement sutup here (steering)
        agent.speed = bugSO.speed;
        agent.angularSpeed = bugSO.angularSpeed;
        agent.acceleration = bugSO.acceleration;
        agent.stoppingDistance = bugSO.stoppingDistance;
        agent.autoBraking = bugSO.autoBraking;
    }

    private void Update()
    { // Adjust this value as needed for the buffer space

        agent.destination = target.position;
    }

    public void BugTakeDamage(int damage)
    {
        if (this.gameObject.activeSelf)
        {
            brain.BugTakeDamage(damage); 
        }
    }

    public void BugSlow()
    {
        if (this.gameObject.activeSelf)
        {
            brain.BugSlow(); 
        }
    }

    public bool GetIsAttackable()
    {
        return brain.GetIsAttackable();
    }

    public void SetIsAttackable(bool isAttackable)
    {
        brain.SetIsAttackable(isAttackable);
    }

    public IEnumerator SpeedDamage()
    {
        agent.speed *= .5f;
        yield return new WaitForSeconds(1f);
        agent.speed = bugSO.speed;
    }

    public int GetHealth()
    {
        return brain.GetHealth();
    }
}
