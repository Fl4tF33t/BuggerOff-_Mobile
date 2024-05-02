using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(BugMovement))]
public class CentipedeBrain : MonoBehaviour
{
    public BugSO bugSO;
    BugMovement bugMovement;

    public int health;
    public int shield;

    private SpriteRenderer[] sprites;
    // Start is called before the first frame update
    private void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
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

    private void ChangeColor(Color color)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }
}
