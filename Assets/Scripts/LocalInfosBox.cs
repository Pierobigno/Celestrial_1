using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class LocalInfosBox : MonoBehaviour
{
    private LocalGamepadButton localGamepadButton;
    private TextMeshPro localInfosBoxText;
    private EventStates eventStates;
    private PlayerInput input;

    [Header("Informations")]
    public bool localInfosBoxIsOpen;

    [Header("Settings")]
    public string wichText1;
    public string wichText2;
    public bool canBeTriggeredSeveralTimes;
    // public ActionToTrigger actionToTrigger;

    [Header("Behavior")]
    public bool changeTextIfTriggered;
    public bool changeColorIfTriggered;
    public Color textTriggeredColor;

    void Awake()
    {
        localGamepadButton = this.transform.GetChild(0).GetComponent<LocalGamepadButton>();
        localInfosBoxText = this.transform.GetChild(1).GetComponent<TextMeshPro>();
        eventStates = GetComponent<EventStates>();
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        input = new PlayerInput();
        input.Player.Enable();
    }

    void Update()
    {
        localInfosBoxText.text = wichText1;

        if(eventStates.isTriggered)
        {
            if(canBeTriggeredSeveralTimes)
            {
                if(changeTextIfTriggered)
                {
                    localInfosBoxText.text = wichText2;
                }

                else if(changeColorIfTriggered)
                {
                    localInfosBoxText.color = textTriggeredColor;
                }
            }

            else
            {
                eventStates.isOver = true;
                transform.parent.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void OpenLocalInfosBox()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        localInfosBoxIsOpen = true;

        if(eventStates != null)
        {
            eventStates.isAble = true;
        }
    }

    public void CloseLocalInfosBox()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        localInfosBoxIsOpen = false;

        if(eventStates != null)
        {
            eventStates.isAble = false;
        }
    }
    // void Attack_canceled()
    // {
    //     if(eventStates != null)
    //     {
    //         if(eventStates.isAble)
    //         {
    //             CloseLocalInfosBox();
    //             eventStates.isTriggered = true;
    //         }
    //     }
    // }

    // void Jump_canceled()
    // {
    //     if(eventStates != null)
    //     {
    //         if(eventStates.isAble)
    //         {
    //             CloseLocalInfosBox();
    //             eventStates.isTriggered = true;
    //         }
    //     }
    // }
    
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.tag == "Player" && !other.isTrigger)
    //     {
    //         OpenLocalInfosBox();
    //         other.GetComponent<CharacterStates>().isAbleToInteractWithEnv = true;
    //     }
    // }
    

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.gameObject.tag == "Player" && !other.isTrigger)
    //     {
    //         CloseLocalInfosBox();
    //         other.GetComponent<CharacterStates>().isAbleToInteractWithEnv = false;
    //     }
    // }
}
