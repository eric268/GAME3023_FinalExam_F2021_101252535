using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeasonName
{
    Spring,
    Summer,
    Fall,
    Winter,
    NUM_OF_SEASONS
}

[CreateAssetMenu(fileName = "New Season", menuName = "Calender/Season")]
public class Season : ScriptableObject
{
    public SeasonName seasonName;
    public bool hasWeather;

    public float dayLightStartHours;
    public float dayLightStartMinutes;

    public float nightTimeStartsHours;
    public float nightTimeStartsMinutes;

    public float daylightIntensity;

    public float nightTimeLightIntensity;

    public Color daylightColor;

    public Color imageColor;
}
