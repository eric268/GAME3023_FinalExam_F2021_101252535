using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventTypes
{
    None,
    Light_Color,
    Light_Intensity,
    Weather,
    Audio,
}

[CreateAssetMenu(fileName = "New Event", menuName = "Calender/SpecialEvents")]
public class CalendarEvents : ScriptableObject
{
    public EventType type;
    WeatherType weatherType;
}
