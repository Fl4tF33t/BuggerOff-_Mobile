using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    

    public event EventHandler OnNextWave;

    //private Levels levels;


    public int spawned;
    public int health;
    public int bugBits;
 
    private void Start()
    {

        health = 10;
        bugBits = 400;

    }


   

    

    public void HealthChange(int amount)
    {
        health += amount;
    }

    public void BugBitsChange(int amount)
    {
        bugBits += amount;
    }

}
