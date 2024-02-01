using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLaunchFight : MonoBehaviour
{
    private FightEngaged fightEngaged;
    private EventStates eventStates;
    public bool endOfEventLaunchFight;
    private bool fightLaunched;
    public bool fightLaunchedDisableEvent;

    void Start()
    {
        fightEngaged = FindObjectOfType<FightEngaged>();
        eventStates = GetComponent<EventStates>(); 
    }

    void Update()
    {
        if(eventStates.isOver && endOfEventLaunchFight && !fightLaunched)
        {
            fightLaunched = true;
            fightEngaged.OnEngageFight();

            if(fightLaunchedDisableEvent)
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
