using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    static int currentDayIndex;
    DayScript[] dayScriptArray;
    Clock clock;

    // Start is called before the first frame update
    void Start()
    {
        dayScriptArray = GetComponentsInChildren<DayScript>();
        totalNumberOfDays = dayScriptArray.Length;
        currentDayIndex = 0;
        PopulateDaysWithNameAndSeason();
        clock = GetComponent<Clock>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hour: " + clock.hourCounter + " Minut: " + clock.minutesCounter);
    }

    public void PopulateDaysWithNameAndSeason()
    {
        int seasonIndex = -1;
        for (int i = 0; i < totalNumberOfDays; i++)
        {
            dayScriptArray[i].dayOfWeek = (DaysOfWeek)(i);
            if (i % 7 == 0)
                seasonIndex++;
            dayScriptArray[i].season = (Seasons)seasonIndex;
        }
    }

}
