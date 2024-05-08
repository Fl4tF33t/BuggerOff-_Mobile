using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStats : Singleton<LoadStats>
{
    public float bugHealth;
    public float bugSpeed;

    public float frogDamage;
    public float frogAttackSpeed;
    public float frogRange;
    public float jumpCoolDown;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name.Contains("Sunny"))
        {
            bugHealth = 10f;
            bugSpeed = 0.1f;

            frogDamage = -5;
            frogAttackSpeed = -0.1f;
        }
        if (SceneManager.GetActiveScene().name.Contains("Rainy"))
        {
            bugSpeed = -0.1f;

            frogDamage = 5f;
        }
        if (SceneManager.GetActiveScene().name.Contains("Foggy"))
        {
            bugSpeed = 0.1f;

            frogRange = -0.3f;
        }
        if (SceneManager.GetActiveScene().name.Contains("Snowy"))
        {
            bugHealth = -5;
            bugSpeed = -0.1f;

            frogAttackSpeed = -0.5f;
        }
    }
}
