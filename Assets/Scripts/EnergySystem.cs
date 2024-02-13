using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergySystem : MonoBehaviour
{
    public TextMeshProUGUI time;
    public Image[] energy = new Image[5];
    bool timerIsRunning = false;
    int count = 0;
    public float timeRemaining = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                time.text = timeRemaining.ToString();
                if (timeRemaining < count)
                {
                    count--;
                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }

        }
        
    }

    

    public void UseEnergy()
    {
        for (int i = energy.Length-1; i > (-1); i--)
        {
            if (energy[i].color == Color.red)
            {
                energy[i].color = Color.white;
                break;
            }
        }
        for (int i = 0; i < energy.Length; i++)
        {
            if (energy[i].color == Color.white)
            {
                count++;
            }
        }
        if (count != 0)
        {
            timeRemaining = count;
        }
        timerIsRunning = true;
    }
}
