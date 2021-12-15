using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Clock : MonoBehaviour
{
    public static Clock m_instance = null;

    public float timeScale;
    public float secondsInMinute;
    public float minutesInHour;
    public float hoursInDay;

    public float seconds;
    public float minutes;
    public float hours;

    public event Action newDayEvent;
    // Start is called before the first frame update
    void Start()
    {
    }

    public static Clock Instance()
    {
        if (m_instance == null)
        {
            m_instance = FindObjectOfType(typeof(Clock)) as Clock;
        }

        return m_instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClock();   
    }

    void UpdateClock()
    {
        seconds += (Time.deltaTime * timeScale);
        if (seconds >= 1)
        {
            seconds = 0;
            minutes++;
            if (minutes >= minutesInHour)
            {
                minutes = 0;
                hours++;
                
                if (hours >= hoursInDay)
                {
                    hours = 0;
                    if (newDayEvent != null)
                    {
                        newDayEvent();
                    }  
                }
            }
        }
    }
}
