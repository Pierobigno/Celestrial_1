using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButtonToPress : MonoBehaviour
{
    private PlayerInput input;
    private EventStates eventStates;
    public ActionToTrigger actionToTrigger;
    private Transform localInfosBox;
    private Transform gameInfosBox;
    private InfosBox infosBox;

    public enum ActionToTrigger
    {
        ActionCanceled,
        JumpCanceled,
        ShootCanceled,
        //Add
    }

    void Start()
    {
        eventStates = GetComponent<EventStates>();
        input = new PlayerInput();
        input.Player.Enable();
        infosBox = FindObjectOfType<InfosBox>();
        gameInfosBox = infosBox.transform;

        if(transform.childCount > 0)
        {
            Debug.Log("Le script EventButtonPress cherche une boîte d'infos locale en Child(0)");
            localInfosBox = transform.GetChild(0);
        }

        //Permet de choisir quelle action doit être réalisée pour déclencher le isTriggered de l'EventStates et quel bouton est affiché
        switch (actionToTrigger)
        {
            case ActionToTrigger.ActionCanceled:
                input.Player.Action.canceled += x => Action_canceled();
                if(gameInfosBox != null)
                {
                    gameInfosBox.GetChild(2).GetComponent<GamepadButtons>().currentSpriteName = "SouthButton";
                }
                if(localInfosBox != null)
                {
                    localInfosBox.GetChild(0).GetComponent<GamepadButtons>().currentSpriteName = "SouthButton";
                }
                break;
            case ActionToTrigger.JumpCanceled:
                input.Player.Jump.canceled += x => Jump_canceled();
                if(gameInfosBox != null)
                {
                    gameInfosBox.GetChild(2).GetComponent<GamepadButtons>().currentSpriteName = "LB";
                }
                if(localInfosBox != null)
                {
                    localInfosBox.GetChild(0).GetComponent<GamepadButtons>().currentSpriteName = "LB";
                }
                break;
            case ActionToTrigger.ShootCanceled:
                input.Player.Shoot.canceled += x => Shoot_canceled();
                if(gameInfosBox != null)
                {
                    gameInfosBox.GetChild(2).GetComponent<GamepadButtons>().currentSpriteName = "RB";
                }
                if(localInfosBox != null)
                {
                    localInfosBox.GetChild(0).GetComponent<GamepadButtons>().currentSpriteName = "RB";
                }
                break;
            // Add

            default:
                input.Player.Action.canceled += x => Action_canceled();
                break;
        }
    }

    void Action_canceled()
    {
        if(eventStates.isAble)
        {
            eventStates.isTriggered = true;
        }

        else if(eventStates.isTriggered)
        {
            eventStates.isOver = true;
        }

        if(localInfosBox != null)
        {
            if(localInfosBox.GetComponent<EventStates>().isAble)
            {
                localInfosBox.GetComponent<EventStates>().isTriggered = true;
                localInfosBox.GetComponent<LocalInfosBox>().CloseLocalInfosBox();
            }
        }
        else if(gameInfosBox != null)
        {
            infosBox.CloseGameInfosBox();
        }
    }

    void Jump_canceled()
    {
        if(eventStates.isAble)
        {
            eventStates.isTriggered = true;
        }

        else if(eventStates.isTriggered)
        {
            eventStates.isOver = true;
        }

        if(localInfosBox != null)
        {
            if(localInfosBox.GetComponent<EventStates>().isAble)
            {
                localInfosBox.GetComponent<EventStates>().isTriggered = true;
                localInfosBox.GetComponent<LocalInfosBox>().CloseLocalInfosBox();
            }
        }
        else if(gameInfosBox != null)
        {
            infosBox.CloseGameInfosBox();
        }
    }

    void Shoot_canceled()
    {
        if(eventStates.isAble)
        {
            eventStates.isTriggered = true;
        }

        else if(eventStates.isTriggered)
        {
            eventStates.isOver = true;
        }

        if(localInfosBox != null)
        {
            if(localInfosBox.GetComponent<EventStates>().isAble)
            {
                localInfosBox.GetComponent<EventStates>().isTriggered = true;
                localInfosBox.GetComponent<LocalInfosBox>().CloseLocalInfosBox();
            }
        }
        else if(gameInfosBox != null)
        {
            infosBox.CloseGameInfosBox();
        }
    }

//     public int inputID;
//     public bool inputTriggerEvent;
//     private CheckButtonPress checkButtonPress;
//     private EventStates eventStates;

//     void Start()
//     {
//         checkButtonPress = FindObjectOfType<CheckButtonPress>();
//         eventStates = GetComponent<EventStates>();
//     }

//     void OnTriggerStay2D(Collider2D other)
//     {
//         if(checkButtonPress.inputID == inputID)
//         {
//             if(inputTriggerEvent)
//             {
//                 eventStates.isTriggered = true;
//             }

//             else
//             {
//                 if(eventStates.isTriggered)
//                 {
//                     eventStates.isOver = true;
//                 }
//             }
//         }
//     }
// }
}

