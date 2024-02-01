using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    private Animator animator;
    private PlayerInput input;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public bool dialogueIsOpen = false;

    private bool nextMessageTimerOn;
    private float nextMessageTimer;

    void Start()
    {
        animator = FindObjectOfType<GameDialogue>().gameObject.GetComponent<Animator>();
        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();
        // actorImage = transform.Find("GameDialogue").Find("Portrait").GetComponent<Image>();
        // actorName = transform.Find("GameDialogue").Find("NameBox").Find("Name").GetComponent<TextMeshProUGUI>();
        // messageText = transform.Find("GameDialogue").Find("DialogueBox").Find("Dialogue").GetComponent<TextMeshProUGUI>();
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        dialogueIsOpen = true;

        Debug.Log("Started conversation " + messages.Length);
        DisplayMessage();

        animator.SetBool("isOpen", true);
    }

    public void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

    }

    public void NextMessage()
    {
        if(!nextMessageTimerOn)
        {
            activeMessage++;
            Debug.Log("La conversation continue");
            if(activeMessage < currentMessages.Length)
            {
                DisplayMessage();
            }
            else
            {
                EndMessage();
                Debug.Log("Conversation ended");
            }
        }

        //Empeche la conversation de se lancer 2 fois de suite (bug introuvable)
        nextMessageTimerOn = true;
    }

    void Update()
    {
        if(nextMessageTimerOn)
        {
            nextMessageTimer += Time.deltaTime;
            if(nextMessageTimer > 0.1f)
            {
                nextMessageTimerOn = false;
            }
        }
    }

    public void EndMessage()
    {
        dialogueIsOpen = false;
        Debug.Log("Le dialogue se ferme");
        animator.SetBool("isOpen", false);
        FindObjectOfType<AudioManager>().PlaySfx(2);
    }
    
    void Action_canceled()
    {
        if(dialogueIsOpen)
        {
            NextMessage();
        }
    }
}
