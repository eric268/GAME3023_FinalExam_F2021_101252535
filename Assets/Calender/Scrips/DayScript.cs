using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class DayScript : MonoBehaviour
{
    public Season season;
    public DaysOfWeek dayOfWeek;
    public Light2D globalLightSource;
    private bool isPastNoon;
    private bool isNightTime, isDayTime;


    [Header("Weather")]
    public List<GameObject> listPossibleWeather;
    GameObject activeWeather;
    public float chanceOfWeather;

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
        CheckDayLightChange();
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
                CheckToAddWeather();
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
                CheckToAddWeather();
            }
        }
    }

    void DeactivateCurrenWeatherSystem()
    {
        if (activeWeather != null && activeWeather.activeInHierarchy)
        {
            activeWeather.SetActive(false);
        }
    }

    void CheckToAddWeather()
    {
        if (listPossibleWeather.Count > 0)
        {
            DeactivateCurrenWeatherSystem();
            float frequencyCheck = Random.Range(0.0f, 1.0f);
            if (frequencyCheck >= (1.0f - chanceOfWeather))
            {

                int selectWeatherType = Random.Range(0, listPossibleWeather.Count);
                listPossibleWeather[selectWeatherType].SetActive(true);
                activeWeather = listPossibleWeather[selectWeatherType];
            }
        }
    }

    private void OnEnable()
    {
        isNightTime = true;
        isDayTime = false;
        globalLightSource.intensity = season.nightTimeLightIntensity;
        globalLightSource.color = season.daylightColor;
    }

    private void OnDisable()
    {
        //if (activeWeather != null && activeWeather.activeInHierarchy)
        //{
        //    activeWeather.SetActive(false);
        //}
    }
}
