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
    public int daysInSeason;

    DayScript[] dayScriptArray;

    public Light2D globalLightSource;
    public Season[] seasonArray;
    public DaysOfWeek currentDay;
    public SeasonName currentSeason;

    //Want to subscribe to this first so that this is called before changes to UI calendar display
    //as day changes will affect the value that those UI elements will acceess
    private void Awake()
    {
        Clock.Instance().newDayEvent += NewDay;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Gets reference to all days in calendar
        dayScriptArray = GetComponentsInChildren<DayScript>();
        totalNumberOfDays = dayScriptArray.Length;
        //Start by disabling all days
        DisableAllDaysOfCalender();
        //Adds the correct name, day number, season, etc attributes to each day
        PopulateDaysWithNameAndSeason();

        //Add correct info for calendar UI elements
        currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
        currentSeason = dayScriptArray[currentDayIndex].season.seasonName;
        GetComponent<TimeDateUI>().NewDay();

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
        StopPlayingPreviousDayAudio();
        dayScriptArray[currentDayIndex++].enabled = false;
        dayScriptArray[currentDayIndex].enabled = true;
        currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
        currentSeason = dayScriptArray[currentDayIndex].season.seasonName;
        ActiveDay = dayScriptArray[currentDayIndex];

    }

    public void PopulateDaysWithNameAndSeason()
    {
        //Start with an index of -1 as 0 % any number will always be true so want to start out season index at 0 not 1 
        int seasonIndex = -1;
        for (int i = 0; i < totalNumberOfDays; i++)
        {
            //Adds day number and day name so that it can be show on top of each day
            dayScriptArray[i].dayNumber = (i + 1);
            dayScriptArray[i].dayOfWeek = (DaysOfWeek)(i % daysInSeason);
            
            //When we have finished with a season we want to increment which season that day belongs to
            if (i % daysInSeason == 0)
            {
                seasonIndex++;
                //If we have more days then a full year we want to start at beginning season
                if (seasonIndex >= seasonArray.Length)
                    seasonIndex = 0;
            }
            //Updates day info or UI etc
            dayScriptArray[i].season = seasonArray[seasonIndex];
            dayScriptArray[i].globalLightSource = globalLightSource;
            dayScriptArray[i].ChangeImageColorToMatchSeason();
            dayScriptArray[i].AddDateTextInfo();
        }
        //Enable starting day (0 index)
        dayScriptArray[currentDayIndex].enabled = true;
        
        //For keeping track of OnValidateChanges
        ActiveDay = dayScriptArray[currentDayIndex];
    }

    //Checks if we have changed which day is active in the inspector
    void CurrentDayChanedInInspector()
    {
        //If active isnt equal to currently active day we know we have changed that currently active day
        if (ActiveDay != null && ActiveDay != dayScriptArray[currentDayIndex])
        {
            //Stop all previous day specific stuff, dont want snow to keep falling if we switched to summer
            StopPlayingPreviousDayAudio();
            ActiveDay.DeactivateCurrenWeatherSystem();
            ActiveDay.enabled = false;

            //Disable and enable correct days
            dayScriptArray[currentDayIndex].enabled = true;
            ActiveDay = dayScriptArray[currentDayIndex];

            //Reset clock for fresh start of each day
            Clock.Instance().hours = 0;
            Clock.Instance().minutes = 0;
            Clock.Instance().seconds = 0;

            //Updated UI calendar values
            currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
            currentSeason = dayScriptArray[currentDayIndex].season.seasonName;
            GetComponent<TimeDateUI>().NewDay();
        }
    }

    private void StopPlayingPreviousDayAudio()
    {
        //Check if the day has an audio event script
        //Since an audiosource is a requirement component then we know it also has an AudioSource component
        if (ActiveDay.GetComponent<AudioEvent>())
        {
            //Stop it from playing
            ActiveDay.GetComponent<AudioSource>().Stop();
        }
    }

    //Makes sure we cant break the game by adding current day indexes outside of our array constraints
    public void OnValidate()
    {
        if (currentDayIndex > totalNumberOfDays - 1 || currentDayIndex < 0)
        {
            currentDayIndex = 0;
        }
        else if (totalNumberOfDays > 0)
        {
            CurrentDayChanedInInspector();
        }
        if (daysInSeason < 0)
            daysInSeason = 1;
    }
}
