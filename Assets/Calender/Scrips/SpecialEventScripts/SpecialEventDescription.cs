using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpecialEventDescription : MonoBehaviour
{
    public GameObject specialEventMessgaeBox;
    public string specialEventMessage = "Special Event: None";

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

    public void MouseExitEventIcon()
    {
        specialEventMessgaeBox.SetActive(false);
    }
}
