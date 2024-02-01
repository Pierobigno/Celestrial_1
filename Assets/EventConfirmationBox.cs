using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventConfirmationWindow : MonoBehaviour
{
    public string confirmationText;
    public string editableText;
    public bool yes;
    public bool no;
    public bool combine;

    private EventStates eventStates;
    private ConfirmationWindow confirmationWindow;

    void Start()
    {
        eventStates = GetComponent<EventStates>();
        confirmationWindow = FindObjectOfType<ConfirmationWindow>();
    }

    void Update()
    {
        if(eventStates.isTriggered && !eventStates.isOver)
        {
            if(!confirmationWindow.isOpen)
            {
                confirmationWindow.OpenConfirmationWindow(confirmationText, editableText, yes, no, combine);
            }
        }

        if(eventStates.isOver)
        {
            confirmationWindow.CloseConfirmationWindow();
        }
    }
}
