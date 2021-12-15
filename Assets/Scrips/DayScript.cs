using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayScript : MonoBehaviour
{
    public bool isCurrentDay;
    public Seasons season;
    public DaysOfWeek dayOfWeek;
    public Season currentSeason;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentDay)
        {
            //TODO
        }
    }

    private void OnEnable()
    {
        isCurrentDay = true;
    }

    private void OnDisable()
    {
        isCurrentDay = false;
    }
}
