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
        time.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(count);

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                //if (Mathf.CeilToInt(timeRemaining / 60) < count + 1)
                //{
                //    if (count > 0)
                //    {
                //        count--;
                //    }
                //}
                if (Mathf.FloorToInt(timeRemaining%60) == 0)
                {
                    count--;
                    energy[energy.Length-1-count].color = Color.red;
                    timeRemaining--;
                    
                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                
            }

        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0; // Ensure time doesn't go negative
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UseEnergy()
    {
        if (count==energy.Length)
        {
            Debug.Log("You are out of energy!!!!!!!");
        }
        int heehee = 0;
        for (int i = energy.Length - 1; i >= 0; i--)
        {
            Debug.Log("i: "+ i);
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
                heehee++;
            }
        }

        if (heehee != 0)
        {
            timeRemaining = timeRemaining + (heehee-count)*60;
            count = heehee;
        }
        timerIsRunning = true;
    }
}
