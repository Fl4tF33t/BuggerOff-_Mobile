using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BugMovement))]
[RequireComponent(typeof(NavMeshAgent))]

public class BugBrain : MonoBehaviour, IBugTakeDamage, IPlayerTakeDamage
{
    public BugSO bugSO;
    NavMeshAgent agent;

    public int health;
    public int shield;

    private SpriteRenderer[] sprites;

    private Coroutine colorDamage;
    private Coroutine speedDamage;

    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        InitializeBugLogic(bugSO);
    }

    private void InitializeBugLogic(BugSO bugSO)
    {
        //set the original values from the SO
        ChangeColor(Color.white);
        health = bugSO.health;
        shield = bugSO.sheild;
    }

    public void BugTakeDamage(int damage)
    {
        CoroutineStarter(colorDamage, "TakeDamage");

        //check if the bug has sheild
        if (shield > 0)


        {
            //if so, reduce the sheild
            shield -= damage;
            //if the sheild is less than 0, set it to 0
            if (shield < 0)
                shield = 0;
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
            GameManager.Instance.BugBitsChange(bugSO.moneyDrop);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TakeDamage()
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(1);
        ChangeColor(Color.white);
    }

    private void ChangeColor(Color color)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        GameManager.Instance.HealthChange(-bugSO.damageToPlayer);
    }

    public void BugSlow()
    {
        CoroutineStarter(speedDamage, "SpeedDamage");
    }

    private IEnumerator SpeedDamage()
    {
        agent.speed *= .5f;
        yield return new WaitForSeconds(1f);
        agent.speed = bugSO.speed;
    }

    private void CoroutineStarter(Coroutine coroutine, string ieNum)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ieNum);
    }
}