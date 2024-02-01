using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIDisplay : MonoBehaviour
{
    private EventStates eventStates;
    public Image[] playerUIImages;
    public bool playerUIIsActive;
    private bool UIDisplayed;

    void Start()
    {
        eventStates = gameObject.GetComponent<EventStates>();
    }

    void Update()
    {
        if(eventStates.isTriggered && !UIDisplayed)
        {
            UIDisplayed = true;
            DisplayPlayerUI();
        }
    }

    void DisplayPlayerUI()
    {
        if(playerUIIsActive)
        {
            foreach (Image playerUIImage in playerUIImages)
            {
                playerUIImage.enabled = false;
            }
        }
        else
        {
            foreach (Image playerUIImage in playerUIImages)
            {
                playerUIImage.enabled = true;
            }
        }
    }
}

