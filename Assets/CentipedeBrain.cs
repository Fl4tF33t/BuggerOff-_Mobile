using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(BugMovement))]
public class CentipedeBrain : MonoBehaviour, IBugTakeDamage
{
    public event Action OnSlow;

    public BugSO bugSO;
    BugMovement bugMovement;

    public int health;
    public int shield;

    private Coroutine colorDamage;
    private Coroutine speedDamage;

    private bool isAttackable = true;

    public SpriteRenderer[] sprites;
    // Start is called before the first frame update
    private void Awake()
    {
        bugMovement = GetComponent<BugMovement>();
    }

    private void OnEnable()
    {
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
        if (this.gameObject.activeSelf)
        {
            CoroutineStarter(colorDamage, "TakeDamage"); 
        }

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
    }

    private void ChangeColor(Color color)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }

    private IEnumerator TakeDamage()
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(1);
        ChangeColor(Color.white);
    }

    private void CoroutineStarter(Coroutine coroutine, string ieNum)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ieNum);
    }

    public void BugSlow()
    {
        if (speedDamage != null)
        {
            StopCoroutine(speedDamage);
        }
        speedDamage = StartCoroutine(bugMovement.SpeedDamage());
        OnSlow?.Invoke();
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
