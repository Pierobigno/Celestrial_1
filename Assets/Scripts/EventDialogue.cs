using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDialogue : MonoBehaviour
{
    private DialogueManager dialogueManager;
    private DialogueTrigger dialogueTrigger;
    private EventStates eventStates;
    private bool timerOn;
    private float timer;
    [HideInInspector] public bool dialogueTriggered;


    public float dialogueDelay;
    public bool endOfDialogueIsEndOfEvent;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        eventStates = GetComponent<EventStates>();
    }

    void Update()
    {
        if(eventStates.isTriggered && !eventStates.isOver)
        {
            if(!dialogueTriggered)
            {
                timerOn = true;
                            
                if(timer > dialogueDelay)
                {
                    dialogueTrigger.StartDialogue();
                    Debug.Log("L'event " + gameObject.name + " lance un dialogue");
                    dialogueTriggered = true;
                    timer = 0f;
                }
            }
        }
        
        if(endOfDialogueIsEndOfEvent)
        {
            if(dialogueTriggered && !dialogueManager.dialogueIsOpen)
            {
                eventStates.isOver = true;
            }
        }
    
        if(timerOn)
        {
            timer += Time.deltaTime;
        }
    }
}
