using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraFollow : MonoBehaviour
{
    private EventStates eventStates;
    private CinemachineController cmController;
    public Transform cameraTarget;
    [HideInInspector] public Transform currentTarget;
    private Transform player;
    private bool triggered;

    private bool timerOn;
    private float timer;
    public float during = 5f;

    private bool timerAfterDialogueOn;
    private float timerAfterDialogue;
    public float duringAfterDialogue = 0.5f;

    public float damping = 5f;

    private DialogueManager dialogueManager;
    public bool waitDialogueEnding;

    void Start()
    {
        cmController = FindObjectOfType<CinemachineController>();
        eventStates = gameObject.GetComponent<EventStates>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(18);
    }

    void Update()
    {
        if(eventStates != null)
        {
            // Si l'event est déclenché
            if(eventStates.isTriggered && !triggered)
            {
                triggered = true;
                timerOn = true;
                SwitchCameraFollow();
            }
        }

        if(timerOn)
        {
            timer += Time.deltaTime;
            if(timer > during)
            {
                // Si la caméra n'a pas besoin d'attendre la fin du dialogue pour revenir
                if(!waitDialogueEnding)
                {
                    timerOn = false;
                    timer = 0;
                    SwitchCameraFollow();
                }
                // Si la caméra doit attendre la fin du dialogue pour revenir
                else
                {
                    if(dialogueManager.dialogueIsOpen)
                    {
                        return;
                    }
                    else
                    {
                        // Lancement du timer d'après dialogue
                        timerAfterDialogueOn = true;
                        timerOn = false;
                    }
                }
            }
        }  
        
        // Timer d'après dialogue (essentiellement pour attendre l'animation de lancement de combat)
        if(timerAfterDialogueOn)
        {
            timerAfterDialogue += Time.deltaTime;
            if(timerAfterDialogue > duringAfterDialogue && !dialogueManager.dialogueIsOpen)
            {
                timerAfterDialogueOn = false;
                timerAfterDialogue = 0;
                SwitchCameraFollow();
            }
        }
    }

    public void SwitchCameraFollow()
    {
        Debug.Log("La caméra change de cible");

        if(cmController.currentTarget == player)
        {
            cmController.currentTarget = cameraTarget;
        }
        else
        {
            cmController.currentTarget = player;
        }
    }
}
