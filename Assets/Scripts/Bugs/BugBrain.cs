using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BugMovement))]

public class BugBrain : BugMethods, IBugTakeDamage, IPlayerTakeDamage
{
    public BugSO bugSO;
    BugMovement bugMovement;

    public int health;
    public int shield;

    private SpriteRenderer[] sprites;

    private Coroutine colorDamageCoroutine;
    private Coroutine slowDownCoroutine;

    private bool isAttackable;

    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        bugMovement = GetComponent<BugMovement>();
    }

    private void OnEnable()
    {
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
        ChangeColor(Color.white, sprites);
        health = bugSO.health;
        shield = bugSO.sheild;

        bugMovement.enabled = true;
        isAttackable = true;
    }

    public void BugTakeDamage(int damage)
    {
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

        //check if it active before starting coroutine
        if (gameObject.activeSelf)
        {
            CoroutineStarter(colorDamageCoroutine, DamageColorChange(sprites));
        }
    }

    public void PlayerTakeDamage(int damage) => GameManager.Instance.HealthChange(-bugSO.damageToPlayer);

    public void BugSlow()
    {
        if (gameObject.activeSelf)
        {
            CoroutineStarter(slowDownCoroutine, bugMovement.SpeedDamage());
        }
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