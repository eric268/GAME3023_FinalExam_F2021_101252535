using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum DaysOfWeek
{
    SUNDAY,
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    NUM_OF_DAYS_PER_WEEK
}

public class CalanderScript : MonoBehaviour
{

    static int totalNumberOfDays;
    public int currentDayIndex;
    public int daysInWeek;

    DayScript[] dayScriptArray;
    Clock clock;
    public Light2D globalLightSource;
    public Season[] seasonArray;

    // Start is called before the first frame update
    void Start()
    {
        dayScriptArray = GetComponentsInChildren<DayScript>();
        totalNumberOfDays = dayScriptArray.Length;
        DisableAllDaysOfCalender();
        PopulateDaysWithNameAndSeason();
        Clock.Instance().newDayEvent += NewDay;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hour: " + Clock.Instance().hours + " Minute: " + Clock.Instance().minutes);
    }

    void DisableAllDaysOfCalender()
    {
        for (int i = 0; i < totalNumberOfDays; i++)
        {
            dayScriptArray[i].enabled = false;
        }
    }

    public void NewDay()
    {
        dayScriptArray[currentDayIndex++].enabled = false;
        dayScriptArray[currentDayIndex].enabled = true;
        Debug.Log("Today is :" + dayScriptArray[currentDayIndex].dayOfWeek + " Season: " + dayScriptArray[currentDayIndex].season.seasonName);
    }

    public void PopulateDaysWithNameAndSeason()
    {
        int seasonIndex = -1;
        for (int i = 0; i < totalNumberOfDays; i++)
        {
            dayScriptArray[i].dayOfWeek = (DaysOfWeek)(i);
            
            if (i % daysInWeek == 0)
                seasonIndex++;

            dayScriptArray[i].season = seasonArray[seasonIndex];
            dayScriptArray[i].globalLightSource = globalLightSource;
            dayScriptArray[i].ChangeImageColorToMatchSeason();
        }

        dayScriptArray[currentDayIndex].enabled = true;
    }

}
