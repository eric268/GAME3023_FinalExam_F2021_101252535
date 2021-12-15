using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum DaysOfWeek
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    NUM_OF_DAYS_PER_WEEK
}

public class CalanderScript : MonoBehaviour
{
    private DayScript ActiveDay;

    static int totalNumberOfDays;
    public int currentDayIndex;
    public int daysInWeek;

    DayScript[] dayScriptArray;
    Clock clock;
    public Light2D globalLightSource;
    public Season[] seasonArray;
    public DaysOfWeek currentDay;
    public SeasonName currentSeason;

    private void Awake()
    {
        Clock.Instance().newDayEvent += NewDay;
    }

    // Start is called before the first frame update
    void Start()
    {
        dayScriptArray = GetComponentsInChildren<DayScript>();
        totalNumberOfDays = dayScriptArray.Length;
        DisableAllDaysOfCalender();
        PopulateDaysWithNameAndSeason();
        currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
        currentSeason = dayScriptArray[currentDayIndex].season.seasonName;
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
        currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
        currentSeason = dayScriptArray[currentDayIndex].season.seasonName;

        ActiveDay = dayScriptArray[currentDayIndex];
    }

    public void PopulateDaysWithNameAndSeason()
    {
        int seasonIndex = -1;
        for (int i = 0; i < totalNumberOfDays; i++)
        {
            dayScriptArray[i].dayOfWeek = (DaysOfWeek)(i % daysInWeek);
            
            if (i % daysInWeek == 0)
                seasonIndex++;

            dayScriptArray[i].season = seasonArray[seasonIndex];
            dayScriptArray[i].globalLightSource = globalLightSource;
            dayScriptArray[i].ChangeImageColorToMatchSeason();
        }

        dayScriptArray[currentDayIndex].enabled = true;
        
        //For keeping track of OnValidateChanges
        ActiveDay = dayScriptArray[currentDayIndex];
    }

    void UpdateCalenderOnInspectorChange()
    {
        if (ActiveDay != null)
            ActiveDay.enabled = false;

        dayScriptArray[currentDayIndex].enabled = true;
        ActiveDay = dayScriptArray[currentDayIndex];

        Clock.Instance().hours = 0;
        Clock.Instance().minutes = 0;
        Clock.Instance().seconds = 0;

        currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
        currentSeason = dayScriptArray[currentDayIndex].season.seasonName;

        GetComponent<TimeDateUI>().NewDay();
    }

    public void OnValidate()
    {
        if (currentDayIndex > totalNumberOfDays - 1)
        {
            currentDayIndex = 0;
        }
        else if (totalNumberOfDays > 0)
        {
            UpdateCalenderOnInspectorChange();
        }
    }
}
