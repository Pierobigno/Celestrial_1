using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGameInfosWindow : MonoBehaviour
{
    [Header("Game Infos Box / QTE")]
    public bool triggerOpenInfosBox;
    public bool endCloseInfosBox;
    public string infosBoxText;
    public string inputSprite;

    private EventStates eventStates;
    private InfosBox infosBox;

    void Start()
    {
        eventStates = GetComponent<EventStates>();
        infosBox = FindObjectOfType<InfosBox>();
    }

    void Update()
    {
        if(eventStates.isTriggered && !eventStates.isOver)
        {
            if(triggerOpenInfosBox && !infosBox.gameInfosBoxIsOpen)
            {
                infosBox.OpenGameInfosBox(inputSprite, infosBoxText);
            }
        }

        if(eventStates.isOver)
        {
            if(endCloseInfosBox)
            {
                infosBox.CloseGameInfosBox();
            }
        }
    }
}
