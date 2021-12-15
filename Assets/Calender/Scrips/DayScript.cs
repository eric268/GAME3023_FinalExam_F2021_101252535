using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DayScript : MonoBehaviour
{
    public bool isCurrentDay;
    public Season season;
    public DaysOfWeek dayOfWeek;
   
    public Light2D globalLightSource;
    private bool isPastNoon;
    private bool isNightTime, isDayTime;
    //public Season currentSeason;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeImageColorToMatchSeason()
    {
        GetComponent<Image>().color = season.imageColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentDay)
        {
            CheckDayLightChange();
        }
    }

    void CheckDayLightChange()
    {
        if (Clock.Instance().hours < 12)
            isPastNoon = false;
        else 
            isPastNoon = true;

        if (!isPastNoon)
        {
            CheckIfDaytime();
        }
        else if (isPastNoon)
        {
            CheckIfNightime();
        }
    }

    void CheckIfDaytime()
    {
        if (!isDayTime)
        {
            if (Clock.Instance().hours >= season.dayLightStartHours && Clock.Instance().minutes > season.dayLightStartMinutes)
            {
                isDayTime = true;
                isNightTime = false;
                globalLightSource.intensity = season.daylightIntensity;
            }
        }
    }

    void CheckIfNightime()
    {
        if (!isNightTime)
        {
            if (Clock.Instance().hours >= season.nightTimeStartsHours && Clock.Instance().minutes > season.nightTimeStartsMinutes)
            {
                isDayTime = false;
                isNightTime = true;
                globalLightSource.intensity = season.nightTimeLightIntensity;
            }
        }
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name + " enabled");
        isCurrentDay = true;
        isNightTime = true;
        isDayTime = false;
        globalLightSource.intensity = season.nightTimeLightIntensity;
        globalLightSource.color = season.daylightColor;
        if (season.hasWeather)
        {
            //Can do a furthur check somewhere if we want a percentage change of weather
            season.weatherController.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Debug.Log(gameObject.name + " disabled");
        isCurrentDay = false;

        if (season.weatherController != null)
        {
            season.weatherController.SetActive(false);
        }
    }
}
