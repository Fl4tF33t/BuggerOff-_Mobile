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
    public Action<bool> OnUpgradeUI;

    public FrogSO frogSO;
    public Frog frog = new Frog();
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

    //UpgradeUI variables 
    Transform canvas;

    //Tracking and targeting variables
    public enum Target
    {
        First,
        Last,
        Strongest,
        Weakest
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
    }

    private void Start()
    {
        OnUpgradeUI = (arg) => { canvas.gameObject.SetActive(arg); };
    }
}
