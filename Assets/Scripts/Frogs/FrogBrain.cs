using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Awake()
    {
        frogSO.InitGameObject(frog);
    }
}
