using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNextEvent : MonoBehaviour
{
    public bool activateIfIsOver;
    public bool activateIfIsAble;
    public bool activateIfIsTriggered;
    public bool disableIfIsNotAble;
    public GameObject[] nextEvents;
    private EventStates eventStates;
    public bool nextEventsActive;

    private float timer;
    public float timerBeforeActivateNextEvent;
    private bool timerOn;

    void Start()
    {
        eventStates = GetComponent<EventStates>();
    }

    void Update()
    {
        if (eventStates.isAble && !nextEventsActive && activateIfIsAble && !timerOn)
        {
            timerOn = true;
        }

        else if(eventStates.isOver && !nextEventsActive && activateIfIsOver && !timerOn)
        {
            timerOn = true;
        }

        else if(eventStates.isTriggered && !nextEventsActive && activateIfIsTriggered && !timerOn)
        {
            timerOn = true;
        }

        else if(!eventStates.isAble && nextEventsActive && disableIfIsNotAble)
        {
            foreach(GameObject nextEvent in nextEvents)
            {
                nextEvent.SetActive(false);
                nextEventsActive = false;
            }
        }
    
        if(timerOn)
        {
            timer += Time.deltaTime;
            if (timer > timerBeforeActivateNextEvent)
            {
                foreach(GameObject nextEvent in nextEvents)
                {
                    nextEvent.SetActive(true);
                    nextEventsActive = true;
                }
                
                timerOn = false;
                timer = 0;
            }
        }

    }
}
