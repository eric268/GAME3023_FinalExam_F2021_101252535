using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

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

    public int dayNumber;
    TextMeshProUGUI dateText;
    public GameObject currentDayIcon;
    public GameObject specialEventIcon;
    public bool hasSpecialEvent;

    public Color dayLightColor;
    public float nightLightIntensity;
    public float dayLightIntensity;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeImageColorToMatchSeason()
    {
        GetComponent<Image>().color = season.imageColor;
    }

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
                globalLightSource.intensity = dayLightIntensity;

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
                globalLightSource.intensity = nightLightIntensity;
                CheckToAddWeather();
            }
        }
    }

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
            DeactivateCurrenWeatherSystem();
            float frequencyCheck = Random.Range(0.01f, 1.0f);
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

        dayLightColor = season.daylightColor;
        nightLightIntensity = season.nightTimeLightIntensity;
        dayLightIntensity = season.daylightIntensity;

        globalLightSource.intensity = nightLightIntensity;
        globalLightSource.color = dayLightColor;
        currentDayIcon.SetActive(true);
    }

    private void OnDisable()
    {
        currentDayIcon.SetActive(false);
    }


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
