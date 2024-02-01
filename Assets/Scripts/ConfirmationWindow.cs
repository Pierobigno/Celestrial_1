using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class ConfirmationWindow : MonoBehaviour
{
    Animator animator;
    PlayerInput input;
    public bool isOpen;
    public GameObject yesInput;
    public GameObject noInput;
    public GameObject combineInput;

    public float confirmationWindowCooldown = 0.2f;
    private float timer;
    private bool timerOn;
    public TextMeshProUGUI confirmation;
    public TextMeshProUGUI question;
    public string answer;

    void Start()
    {
        animator = GetComponent<Animator>();

        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();
        input.Player.Bomb.canceled += x => Bomb_canceled();
    }

    public void OpenConfirmationWindow(string confirmationText, string editableText, bool yes, bool no, bool combine)
    {
        if(!isOpen)
        {
            animator.SetBool("isOpen", true);
            isOpen = true;
            timerOn = true;

            confirmation.text = confirmationText;
            question.text = editableText;

            yesInput.SetActive(yes);
            noInput.SetActive(no);
            combineInput.SetActive(combine);

            transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("La boite de confirmation s'ouvre");
        }
    }


    public void Accept()
    {
        answer = "Accept";
        CloseConfirmationWindow();
    }

    public void Refuse()
    {
        answer = "Refuse";
        CloseConfirmationWindow();
    }    

    public void CloseConfirmationWindow()
    {
        if(isOpen)
        {
            StartCoroutine(CloseConfirmationWindowCoroutine());
        }        
    }  

    IEnumerator CloseConfirmationWindowCoroutine()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("La fenêtre de confirmation se ferme");
        yield return new WaitForSeconds(0.25f); // Empeche de faire 2 actions en meme temps en appuyant sur A
        transform.GetChild(0).gameObject.SetActive(false);
        isOpen = false;
        answer = ""; // Laisse le temps de prendre en compte la réponse
    }

    void Action_canceled()
    {
        if(isOpen && !timerOn)
        {
            Accept();
        }
    }

    void Bomb_canceled()
    {
        if(isOpen && !timerOn)
        {
            Refuse();
        }

    }

    void Update()
    {
        if(timerOn)
        {
            timer += Time.unscaledDeltaTime;
            if(timer > confirmationWindowCooldown)
            {
                timer = 0;
                timerOn = false;
            }
        }
    }
}
