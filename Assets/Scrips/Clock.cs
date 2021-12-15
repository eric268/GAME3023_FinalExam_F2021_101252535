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

    public float secondsCounter;
    public float minutesCounter;
    public float hourCounter;

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
        secondsCounter += (Time.deltaTime * timeScale);
        if (secondsCounter >= 1)
        {
            secondsCounter = 0;
            minutesCounter++;
            if (minutesCounter >= minutesInHour)
            {
                minutesCounter = 0;
                hourCounter++;
                
                if (hourCounter >= hoursInDay)
                {
                    hourCounter = 0;
                    if (newDayEvent != null)
                    {
                        newDayEvent();
                    }  
                }
            }
        }
    }
}
