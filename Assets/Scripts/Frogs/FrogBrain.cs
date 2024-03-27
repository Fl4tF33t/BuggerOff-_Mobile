using System;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BehaviourTreeRunner))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]

public class FrogBrain : MonoBehaviour
{
    public event Action<string> OnAnimationTrigger;

    public Action<bool> OnUpgradeUI;
    public Action<string> OnTriggerEvent;

    private SpriteRenderer[] sprites;

    public FrogSO frogSO;
    public Frog frog = new Frog();
    public FrogUpgrade frogUpgrade = new FrogUpgrade();
    [Serializable]
    public class Frog
    {
        public string frogName;
        public int discipline;
        public int damage;
        public float range;
        public float attackSpeed;
        public Target target;
    }
    [Serializable]
    public class FrogUpgrade
    {
        public int disciplineLevel;
        public int damageLevel;
        public int rangeLevel;
        public int attackSpeedLevel;
    }

    //UpgradeUI variables 
    Transform canvas;

    //Attacking variables
    public AttackType attackType;
    [HideInInspector]
    public Transform projectilePos;
    [HideInInspector]
    public GameObject projectile;

    //Tracking and targeting variables
    [Serializable]
    public enum Target
    {
        First,
        Last,
        Strongest,
        Weakest
    }

    [Serializable]
    public enum AttackType
    {
        AOE,
        Projectile,
        Single,
        Other,
    }
    private void Awake()
    {
        frogSO.InitGameObject(frog);
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.name == "UpgradeCanvas")
            {
                canvas = transform.GetChild(i);
            }
        }

        sprites = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        OnUpgradeUI = (arg) => { canvas.gameObject.SetActive(arg); };
        OnTriggerEvent = (arg) => { OnAnimationTrigger?.Invoke(arg); };
    }

    public void SpawnFrog()
    {
        GetComponent<BehaviourTreeRunner>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
        ChangeColor(Color.white);
    }

    public void ChangeColor(Color color)
    {
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.color = color;
        }
    }
}
