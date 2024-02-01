using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InfosBox : MonoBehaviour
{
    private GamepadButtons gamepadButtons;
    private TextMeshProUGUI infosBoxText;
    private PlayerInput input;
    private CheckButtonPress checkButtonPress;
    private Animator animator;

    // public enum ActionToTrigger
    // {
    //     ActionCanceled,
    //     JumpCanceled,
    //     ShootCanceled,
    //     //Add
    // }

    [Header("Informations")]
    public bool gameInfosBoxIsOpen;

    // [Header("Settings")]
    // public string wichText;
    // public string wichButton; //Set with actionToTrigger but can be manualy removed to QTE
    // // public ActionToTrigger actionToTrigger;

    void Awake()
    {
        gamepadButtons = this.transform.GetChild(2).GetComponent<GamepadButtons>();
        checkButtonPress = FindObjectOfType<CheckButtonPress>();
        infosBoxText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    // void Start()
    // {
    //     // for (int i = 0; i < transform.childCount; i++)
    //     // {
    //     //     transform.GetChild(i).gameObject.SetActive(false);
    //     // }

    //     input = new PlayerInput();
    //     input.Player.Enable();

    //     //Permet de choisir quelle action doit être réalisée pour déclencher le isTriggered de l'EventStates et quel bouton est affiché
    //     switch (actionToTrigger)
    //     {
    //         case ActionToTrigger.ActionCanceled:
    //             input.Player.Action.canceled += x => Action_canceled();
    //             wichButton = "SouthButton";
    //             break;
    //         case ActionToTrigger.JumpCanceled:
    //             input.Player.Jump.canceled += x => Jump_canceled();
    //             wichButton = "NorthButton";
    //             break;
    //         // Add

    //         default:
    //             input.Player.Action.canceled += x => Action_canceled();
    //             break;
    //     }
    // }

    // void Update()
    // {
    //     infosBoxText.text = wichText;
    //     gamepadButtons.currentSpriteName = wichButton;
    // }

    public void OpenGameInfosBox(string wichButton, string wichText)
    {
        animator.SetBool("isOpen", true);
        gameInfosBoxIsOpen = true;
        gamepadButtons.currentSpriteName = wichButton;
        infosBoxText.text = wichText;
        Debug.Log("La GameInfosBox s'ouvre");
    }

    public void CloseGameInfosBox()
    {
        animator.SetBool("isOpen", false);
        gameInfosBoxIsOpen = false;
        checkButtonPress.inputID = 0;
        Debug.Log("La GameInfosBox se ferme");
    }
    
    void Action_canceled()
    {
        if(gameInfosBoxIsOpen)
        {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            if(!dialogueManager.dialogueIsOpen)
            {
                CloseGameInfosBox();
            }
        }        
    }

    void Jump_canceled()
    {
        CloseGameInfosBox();
    }

    void Shoot_canceled()
    {
        CloseGameInfosBox();
    }

    void InvokeMunin_canceled()
    {
        CloseGameInfosBox();
    }

}
