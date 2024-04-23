using System;
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
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

    public SpriteRenderer[] sprites;

    public FrogSO frogSO;
    public Frog frog = new Frog();

    public FrogUpgrade frogUpgrade = new FrogUpgrade();
    public BuffValue buffValue = new BuffValue();
    [Serializable]
    public class Frog
    {
        public string frogName;
        public int discipline;
        public int damage;
        public float range;
        public float attackSpeed;
        public LogicSO.Target target;
    }
    [Serializable]
    public class FrogUpgrade
    {
        public int disciplineLevel;
        public int damageLevel;
        public int rangeLevel;
        public int attackSpeedLevel;
    }
    [Serializable]
    public class BuffValue
    {
        public int damage;
        public float range;
        public float attackSpeed;
    } 

    //UpgradeUI variables 
    Transform canvas;

    //Attacking variables
    public LogicSO.AttackType attackType;
    [HideInInspector]
    public Transform projectilePos;
    [HideInInspector]
    public GameObject projectile;

    //Tracking and targeting variables
    [HideInInspector]
    public Transform singleAttack;
    [HideInInspector]
    public bool isBuffed;
    private bool setBuff;
    private bool setDebuff;
    private void Awake()
    {
        frogSO.InitGameObject(frog);
        Debug.Log("Now init");
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
        OnUpgradeUI = (arg) => { canvas.gameObject.SetActive(arg);
            canvas.transform.Find("DamageConfirm").gameObject.SetActive(false);
            canvas.transform.Find("AttackSpeedConfirm").gameObject.SetActive(false);
            canvas.transform.Find("RangeConfirm").gameObject.SetActive(false);
            canvas.transform.Find("DisciplineConfirm").gameObject.SetActive(false);
        };
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

    private void Update()
    {
        if(isBuffed && !setBuff)
        {
            ChangeColor(Color.magenta);
            frog.damage = frog.damage + buffValue.damage;
            frog.range = frog.range + buffValue.range;
            frog.attackSpeed = frog.attackSpeed + buffValue.attackSpeed;

            setBuff = true;

            setDebuff = false;
        }
        if(!isBuffed && !setDebuff)
        {
            ChangeColor(Color.white);

            frog.damage = frog.damage - buffValue.damage;
            frog.range = frog.range - buffValue.range;
            frog.attackSpeed = frog.attackSpeed - buffValue.attackSpeed;

            setDebuff = true;

            setBuff = false;
        }
    }
}
