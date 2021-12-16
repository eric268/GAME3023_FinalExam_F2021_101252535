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
    GameObject[] weatherRefArray;
    Clock clock;
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
        weatherRefArray = GameObject.FindGameObjectsWithTag("Weather");
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

    void UpdateDayChanged()
    {
        if (ActiveDay != null && ActiveDay != dayScriptArray[currentDayIndex])
        {
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

    public void OnValidate()
    {
        if (currentDayIndex > totalNumberOfDays - 1)
        {
            currentDayIndex = 0;
        }
        else if (totalNumberOfDays > 0)
        {
            UpdateDayChanged();
        }
    }

    void MoveCurrentDayIcon(DayScript day, IconCornerPos pos)
    {
        currentDayIcon.transform.SetParent(day.transform);
        currentDayIcon.transform.localPosition = IconPositions.GetCornerOffset(pos);
    }
}
