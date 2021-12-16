using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalLightEventType
{
    Change_DayLight_Intensity,
    Change_EveningLight_Intensity,
    Change_DayLight_Color
}

[RequireComponent(typeof(DayScript))]
public class GloablLightEvent : MonoBehaviour
{
    public GlobalLightEventType eventType;
    public float dayLightIntensity;
    public float nightLightIntensity;
    public Color dayLightColor;

    private DayScript dayScript;

    private bool activateScript;

    private void Start()
    {
        dayScript = GetComponent<DayScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dayScript.enabled == true && !activateScript)
        {
            activateScript = true;
            CheckWhichLightEventToActivate(eventType);
        }
        else if (dayScript.enabled == false)
        {
            activateScript = false;
        }
    }

    void CheckWhichLightEventToActivate(GlobalLightEventType type)
    {
        switch (type)
        {
            case GlobalLightEventType.Change_DayLight_Intensity:
                dayScript.dayLightIntensity = dayLightIntensity;
                break;
            case GlobalLightEventType.Change_EveningLight_Intensity:
                dayScript.nightLightIntensity = nightLightIntensity;
                break;
            case GlobalLightEventType.Change_DayLight_Color:
                dayScript.globalLightSource.color = dayLightColor;
                break;
        }

    }
}
