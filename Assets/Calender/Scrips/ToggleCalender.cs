using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCalender : MonoBehaviour
{
    GameObject calendarObject;
    bool isVisible = true;
    RectTransform rectTransform;
    float startingWidth;
    float startingHeight;
    public GameObject calendarTextBoarder;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startingWidth = GetComponent<RectTransform>().rect.width;
        startingHeight = GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //This is a workaround for not disabling the calendar when toggling off
    //This is needed because the active day script needs to run even while calendar isnt show
    //This stops calendar from being shown but still allows attached active scripts to run
    public void ToggleRenderer()
    {
        isVisible = !isVisible;
        if (isVisible)
        {
            rectTransform.sizeDelta = new Vector2(startingWidth, startingHeight);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(0, 0);
        }
        calendarTextBoarder.SetActive(isVisible);
    }
}
