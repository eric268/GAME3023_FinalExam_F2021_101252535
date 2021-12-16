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

    public Light2D globalLightSource;
    public Season[] seasonArray;
    public DaysOfWeek currentDay;
    public SeasonName currentSeason;

    public GameObject currentDayIcon;


    private void Awake()
    {
        Clock.Instance().newDayEvent += NewDay;
    }

    // Start is called before the first frame update
    void Start()
    {
        dayScriptArray = GetComponentsInChildren<DayScript>();
        totalNumberOfDays = dayScriptArray.Length;
        currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
        currentSeason = dayScriptArray[currentDayIndex].season.seasonName;
        DisableAllDaysOfCalender();
        PopulateDaysWithNameAndSeason();
        GetComponent<TimeDateUI>().NewDay();
    }

    // Update is called once per frame
    void Update()
    {
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
        MoveCurrentDayIcon(dayScriptArray[currentDayIndex], IconCornerPos.Bottom_Right);
        ActiveDay = dayScriptArray[currentDayIndex];

    }

    public void PopulateDaysWithNameAndSeason()
    {
        int seasonIndex = -1;
        for (int i = 0; i < totalNumberOfDays; i++)
        {
            dayScriptArray[i].dayNumber = (i + 1);
            dayScriptArray[i].dayOfWeek = (DaysOfWeek)(i % daysInWeek);
            
            if (i % daysInWeek == 0)
                seasonIndex++;

            dayScriptArray[i].season = seasonArray[seasonIndex];
            dayScriptArray[i].globalLightSource = globalLightSource;
            dayScriptArray[i].ChangeImageColorToMatchSeason();
            dayScriptArray[i].AddDateTextInfo();
        }

        dayScriptArray[currentDayIndex].enabled = true;
        
        //For keeping track of OnValidateChanges
        ActiveDay = dayScriptArray[currentDayIndex];
    }

    void CurrentDayChanedInInspector()
    {
        if (ActiveDay != null && ActiveDay != dayScriptArray[currentDayIndex])
        {
            StopPlayingPreviousDayAudio();
            ActiveDay.DeactivateCurrenWeatherSystem();
            ActiveDay.enabled = false;

            dayScriptArray[currentDayIndex].enabled = true;
            ActiveDay = dayScriptArray[currentDayIndex];

            MoveCurrentDayIcon(dayScriptArray[currentDayIndex], IconCornerPos.Top_Right);

            Clock.Instance().hours = 0;
            Clock.Instance().minutes = 0;
            Clock.Instance().seconds = 0;

            currentDay = dayScriptArray[currentDayIndex].dayOfWeek;
            currentSeason = dayScriptArray[currentDayIndex].season.seasonName;

            GetComponent<TimeDateUI>().NewDay();
        }
    }

    void MoveCurrentDayIcon(DayScript day, IconCornerPos pos)
    {
        currentDayIcon.transform.SetParent(day.transform);
        currentDayIcon.transform.localPosition = IconPositions.GetCornerOffset(pos);
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


    public void OnValidate()
    {
        if (currentDayIndex > totalNumberOfDays - 1)
        {
            currentDayIndex = 0;
        }
        else if (totalNumberOfDays > 0)
        {
            CurrentDayChanedInInspector();
        }
    }
}
