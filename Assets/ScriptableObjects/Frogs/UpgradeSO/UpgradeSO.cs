using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "ScriptableObjects/Frogs/UpgradeSO", order = 4)]
public class UpgradeSO : ScriptableObject
{
    //This scriptable object is in charge for everything upgrade related to the frogSO
    [Header("Upgrade")]
    public Upgrade<int> discipline;
    public Upgrade<int> damage;
    public Upgrade<float> range;
    public Upgrade<float> attackSpeed;

    public enum UpgradeAmount { Zero, One, Two, Three }

    [Serializable]
    public struct Upgrade<T>
    {
        [Min(0)]
        public T max;
        [Min(0)]
        public T amount;
        public UpgradeAmount increment;
    }

}
