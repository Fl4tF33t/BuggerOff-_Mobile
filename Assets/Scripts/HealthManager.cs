using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public Image shieldBar;
    public GameObject shield;
    public BugBrain info;
    private int bugHealth;
    private int bugShield;
    private float perPercentageHealth;
    private float perPercentageShield;

    void Start()
    {
        bugHealth = info.health;
        bugShield = info.shield;
        perPercentageHealth = bugHealth / 100f;
        perPercentageShield = bugShield / 100f;
        if (bugShield == 0)
        {
            shield.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bugHealth = info.health;
        bugShield = info.shield;
        healthBar.fillAmount = (bugHealth / perPercentageHealth) / 100f;
        shieldBar.fillAmount = (bugShield / perPercentageShield) / 100f;
    }


}
