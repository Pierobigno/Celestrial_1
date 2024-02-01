using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnter : MonoBehaviour
{
    private EventStates eventStates;
    private EventDialogue eventDialogue;
    private CharacterStates playerCharacterStates;

    [Header("Settings")]
    public bool isAble;
    public bool isTriggered;
    public bool isOver;
    public bool resetEvent;

    [Tooltip("Booléen créé pour éviter que le joueur attaque en même temps.")]
    public bool playerIsAbleToInteractWithEnv;
    private Transform localInfosBox;

    void Start()
    {
        eventStates = GetComponent<EventStates>();
        playerCharacterStates = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStates>();
        eventDialogue = GetComponent<EventDialogue>();
        
        if(transform.childCount > 0)
        {
            localInfosBox = transform.GetChild(0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(!other.isTrigger)
            {
                if(isAble)
                {
                    eventStates.isAble = true;
                }

                if(isTriggered)
                {
                    eventStates.isTriggered = true;
                }

                if(isOver)
                {
                    eventStates.isOver = true;
                }

                if(resetEvent)
                {
                    eventStates.isTriggered = false;
                    eventStates.isOver = false;
                    if(eventDialogue != null)
                    {
                        eventDialogue.dialogueTriggered = false;
                    }
                }

                if(playerIsAbleToInteractWithEnv)
                {
                    playerCharacterStates.isAbleToInteractWithEnv = true;
                }
            }
        }
        
        if(!other.isTrigger)
        {
            if(localInfosBox != null && localInfosBox.name == "LocalInfosBox")
            {
                localInfosBox.GetComponent<LocalInfosBox>().OpenLocalInfosBox();
            }
        }
    }
}
