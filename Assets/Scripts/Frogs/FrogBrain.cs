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
    [SerializeField]
    GameObject canvas;

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
    }

    public void UpgradeUI(bool arg)
    {
        canvas.SetActive(arg);
    }
}
