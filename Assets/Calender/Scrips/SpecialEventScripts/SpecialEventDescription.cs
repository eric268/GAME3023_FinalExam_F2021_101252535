using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This class is used to display the messages that describe special day events
public class SpecialEventDescription : MonoBehaviour
{
    public GameObject specialEventMessgaeBox;
    public string specialEventMessage = "Special Event: None";

    //Start showing text box and message
    public void MouseEnterEventIcon()
    {
        if (specialEventMessgaeBox)
        {
            specialEventMessgaeBox.SetActive(true);
            specialEventMessgaeBox.GetComponentInChildren<TextMeshProUGUI>().text = specialEventMessage;
        }
        else
        {
            Debug.Log("Unable to find special event message box game object to special event icon");
        }

    }
    //Stop showing text box and message
    public void MouseExitEventIcon()
    {
        specialEventMessgaeBox.SetActive(false);
    }
}
