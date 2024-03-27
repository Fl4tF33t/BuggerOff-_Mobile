using System;
using System.Collections;
using System.Collections.Generic;
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

    [Serializable]
    public struct Upgrade<T>
    {
        [Min(0)]
        public T amount;
        [Min(0)]
        public T price;
    }

}
