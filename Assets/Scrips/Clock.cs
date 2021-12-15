using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float timeScale;
    public float secondsInMinute;
    public float minutesInHour;
    public float hoursInDay;

    public float secondsCounter;
    public float minutesCounter;
    public float hourCounter;
    // Start is called before the first frame update
    void Start()
    {
        
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
                
                if (hourCounter > hoursInDay)
                {
                    hourCounter = 0;
                    //Broadcast some type of event
                }
            }
        }
    }
}
