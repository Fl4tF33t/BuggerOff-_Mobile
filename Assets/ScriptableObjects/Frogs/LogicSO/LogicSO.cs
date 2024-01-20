using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LogicSO", menuName = "ScriptableObjects/Frogs/LogicSO", order = 1)]
public class LogicSO : ScriptableObject
{
    //This scriptable object is in charge for everything logic related to the frogSO

    [Header("LOGIC")]

    public string frogName;
    [Min(50)]
    public int cost;
    [Range(0, 6)]
    public int discipline;
    [Min(5)]
    public int damage;
    [Min(1)]
    public float range;
    [Min(0.5f)]
    public float attackSpeed;
    public LayerMask targetLayer;

}
