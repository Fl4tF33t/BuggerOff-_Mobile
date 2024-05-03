using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BugMovement))]

public class BugBrain : MonoBehaviour, IBugTakeDamage, IPlayerTakeDamage
{
    public BugSO bugSO;
    BugMovement bugMovement;

    public int health;
    public int shield;

    private SpriteRenderer[] sprites;

    private Coroutine colorDamage;
    private Coroutine speedDamage;

    private bool isAttackable;

    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        bugMovement = GetComponent<BugMovement>();
    }

    private void OnEnable()
    {
        isAttackable = true;
        bugMovement.enabled = true;
        InitializeBugLogic(bugSO);
    }

    private void OnDisable()
    {
        bugMovement.enabled = false;
        StopAllCoroutines();
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
        if (speedDamage != null)
        {
            StopCoroutine(speedDamage);
        }
        speedDamage = StartCoroutine(bugMovement.SpeedDamage());
    }

    private void CoroutineStarter(Coroutine coroutine, string ieNum)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ieNum);
    }

    public bool GetIsAttackable()
    {
        return isAttackable;
    }

    public void SetIsAttackable(bool isAttackable)
    {
        this.isAttackable = isAttackable;
    }

    public int GetHealth()
    {
        return health;  
    }
}