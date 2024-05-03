using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(BugMovement))]
public class CentipedeBrain : BugMethods, IBugTakeDamage
{
    public event Action OnSlow;

    public BugSO bugSO;
    BugMovement bugMovement;

    public int health;
    public int shield;

    private Coroutine colorDamageCoroutine;
    private Coroutine slowDownCoroutine;

    private bool isAttackable = true;

    public SpriteRenderer[] sprites;
    // Start is called before the first frame update
    private void Awake()
    {
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

        isAttackable = true;
        bugMovement.enabled = true;
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
            transform.parent.gameObject.SetActive(false);
        }

        if (this.gameObject.activeSelf)
        {
            CoroutineStarter(colorDamageCoroutine, DamageColorChange(sprites));
        }
    }

    public void BugSlow()
    {
        if (gameObject.activeSelf)
        {
            CoroutineStarter(slowDownCoroutine, bugMovement.SpeedDamage());
            OnSlow?.Invoke();
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
