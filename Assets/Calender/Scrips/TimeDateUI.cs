using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDateUI : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI seasonText;
    CalanderScript calander;
    // Start is called before the first frame update
    void Start()
    {
        Clock.Instance().newDayEvent += NewDay;
        calander = GetComponent<CalanderScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.SetText("Time: {0:00} : {1:00}", Clock.Instance().hours, Clock.Instance().minutes);
    }

    public void NewDay()
    {
        dayText.text = "Current Day: " + calander.currentDay.ToString();
        seasonText.text = "Season: " + calander.currentSeason.ToString();
    }
}
