using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class DayScript : MonoBehaviour
{
    [Header("Season")]
    public Season season;
    public DaysOfWeek dayOfWeek;
    public Light2D globalLightSource;
    public Color dayLightColor;
    public float nightLightIntensity;
    public float dayLightIntensity;

    [Header("Weather")]
    public List<GameObject> listPossibleWeather;
    GameObject activeWeather;
    public float chanceOfWeather;

    [Header("Day UI")]
    public int dayNumber;
    TextMeshProUGUI dateText;
    public GameObject currentDayIcon;
    public GameObject specialEventIcon;

    [Header("Special Event")]
    public bool hasSpecialEvent;

    private bool isPastNoon;
    private bool isNightTime, isDayTime;

    //Changes day icon to correct color
    public void ChangeImageColorToMatchSeason()
    {
        GetComponent<Image>().color = season.imageColor;
    }

    //Adds correct day and 3 letter name UI to each day
    public void AddDateTextInfo()
    {
        dateText = GetComponentInChildren<TextMeshProUGUI>();
        if (dateText != null)
        {
            dateText.text = dayOfWeek.ToString().Substring(0, 3) + "\n" + dayNumber;
            dateText.gameObject.transform.localPosition = IconPositions.GetCornerOffset(IconCornerPos.Top_Center);
        }
        else
        {
            Debug.LogError("Day number: " + dayNumber + " missing date text component");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckDayLightChange();
    }

    //Check if changed to night or day time
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
        //Only want to check if we should change from night to daytime when it isnt daytime
        if (!isDayTime)
        {
            if (Clock.Instance().hours >= season.dayLightStartHours && Clock.Instance().minutes > season.dayLightStartMinutes)
            {
                isDayTime = true;
                isNightTime = false;
                globalLightSource.intensity = dayLightIntensity;

                //Checks for weather changes
                CheckToAddWeather();
            }
        }
    }

    void CheckIfNightime()
    {
        //Only want to check when currently daytime
        if (!isNightTime)
        {
            if (Clock.Instance().hours >= season.nightTimeStartsHours && Clock.Instance().minutes > season.nightTimeStartsMinutes)
            {
                isDayTime = false;
                isNightTime = true;
                globalLightSource.intensity = nightLightIntensity;
                CheckToAddWeather();
            }
        }
    }

    //Used to disable all weather systems
    public void DeactivateCurrenWeatherSystem()
    {
        foreach(GameObject weather in listPossibleWeather)
        {
            weather.SetActive(false);
        }
    }

    void CheckToAddWeather()
    {
        if (listPossibleWeather.Count > 0)
        {
            //Don't want multiple weather systems playing at once
            DeactivateCurrenWeatherSystem();

            //Does randomization to see if weather should play
            float frequencyCheck = Random.Range(0.01f, 1.0f);
            if (frequencyCheck >= (1.0f - chanceOfWeather))
            {
                //Randomly selects a weather system to play if there are multiple
                int selectWeatherType = Random.Range(0, listPossibleWeather.Count);
                listPossibleWeather[selectWeatherType].SetActive(true);
                activeWeather = listPossibleWeather[selectWeatherType];
            }
        }
    }

    //Activates a lot of day specific functionality and attributes
    private void OnEnable()
    {
        isNightTime = true;
        isDayTime = false;

        dayLightColor = season.daylightColor;
        nightLightIntensity = season.nightTimeLightIntensity;
        dayLightIntensity = season.daylightIntensity;

        globalLightSource.intensity = nightLightIntensity;
        globalLightSource.color = dayLightColor;
        currentDayIcon.SetActive(true);
    }

    //Stop showing that it is the current day
    private void OnDisable()
    {
        currentDayIcon.SetActive(false);
    }

    //Show the current day icon is has special event is true
    private void OnValidate()
    {
        if (hasSpecialEvent)
        {
            specialEventIcon.SetActive(true);
        }
        else
            specialEventIcon.SetActive(false);
    }
}
