using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExit : MonoBehaviour
{
    private EventStates eventStates;
    private EventEffects eventEffects;
    private EventDialogue eventDialogue;
    private ActivateNextEvent activateNextEvent;
    
    [Header("Settings")]
    public bool disableIsAble;
    public bool disableEvent;
    public bool resetEvent;
    public bool disableIsAbleToIntercatWithEnv;
    public bool exitActivateNextEvent;
    private Transform localInfosBox;

    void Start()
    {
        eventStates = GetComponent<EventStates>();
        eventEffects = GetComponent<EventEffects>();
        eventDialogue = GetComponent<EventDialogue>();
        activateNextEvent = GetComponent<ActivateNextEvent>();
        
        if(transform.childCount > 0)
        {
            localInfosBox = transform.GetChild(0);
        }
        
        if(exitActivateNextEvent)
        {
            activateNextEvent.enabled = false;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(!other.isTrigger)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                eventStates.isAble = false;

                if(eventStates.isOver)
                {
                    if(disableEvent)
                    {
                        GetComponent<Collider2D>().enabled = false;
                    }
                }

                if(resetEvent)
                {
                    eventStates.isTriggered = false;
                    eventStates.isOver = false;
                    if(eventDialogue != null)
                    {
                        eventDialogue.dialogueTriggered = false;
                    }

                    if(eventEffects != null)
                    {
                        if(eventEffects.animatedObject != null)
                        {
                            eventEffects.animatedObject.GetComponent<Animator>().SetBool("isTriggered", false);
                        }
                    }
                }
                
                if(exitActivateNextEvent)
                {
                    if(activateNextEvent != null)
                    {
                        activateNextEvent.enabled = true;
                    }
                }

                if(disableIsAble)
                {
                    eventStates.isAble = false;
                }

                if(disableIsAbleToIntercatWithEnv)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStates>().isAbleToInteractWithEnv = false;
                }

            }
        }

        if(!other.isTrigger)
        {
            if(localInfosBox != null)
            {
                localInfosBox.GetComponent<LocalInfosBox>().CloseLocalInfosBox();
            }
        }
    }
}
