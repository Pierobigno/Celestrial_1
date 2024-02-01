using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadMenuController : MonoBehaviour
{
    private Gamepad gamepad;
    private float timer;
    private bool timerOn;
    private Vector2 direction;
    private PlayerInput input;
    private ConfirmationWindow confirmationWindow;
    private Animator loadMenuAnimator;
    private bool isActive;
    private List<GameObject> loadSlots;

    public GridLayoutGroup gridLayout;
    public GameObject loadMenu;
    public GameObject cursor;

    [Header("Generalités")]
    public int targetButton;

    void Awake()
    {
        gamepad = Gamepad.current;
        confirmationWindow = FindObjectOfType<ConfirmationWindow>();
        loadMenuAnimator = loadMenu.GetComponent<Animator>();

        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();
        input.Player.Bomb.canceled += x => Bomb_canceled();
    }

    void Start()
    {
        loadSlots = new List<GameObject>();
        InitializeLoadSlots();
    }

    void Update()
    {
        if(gamepad != null && isActive)
        {
            direction = gamepad.leftStick.ReadValue() * -1;

            if(loadSlots.Count > 0)
            {
                cursor.transform.position = loadSlots[targetButton].transform.position;
            }

            if(targetButton < (loadSlots.Count - 1) && direction.y > 0 && !timerOn)
            {
                targetButton += 1;
                timerOn = true;
            }

            if(targetButton > 0 && direction.y < 0 && !timerOn)
            {
                targetButton -= 1;
                timerOn = true;
            }
        }

        if(timerOn)
        {
            timer += Time.unscaledDeltaTime;
            if(timer > 0.2f)
            {
                timerOn = false;
                timer = 0;
            }
        }
    }

    public void InitializeCursorPosition()
    {
        targetButton = 0;
    }

    public void ResetCursorPosition()
    {
        targetButton = 0;
    }

    void InitializeLoadSlots()
    {
        if(loadSlots.Count > 0)
        {
            foreach (Transform child in transform.GetChild(0))
            {
                loadSlots.Add(child.gameObject);
            }
        }
    }
    
    public void OpenLoadMenu()
    {
        isActive = true;
        loadMenu.SetActive(true);
        InitializeLoadSlots();
        InitializeCursorPosition();
        loadMenuAnimator.SetBool("isOpen", true);
    }
    
    public void CloseLoadMenu()
    {
        StartCoroutine(CloseLoadMenuCoroutine());
    }

    IEnumerator CloseLoadMenuCoroutine()
    {
        isActive = false;
        loadMenuAnimator.SetBool("isOpen", false);
        ResetCursorPosition();
        yield return new WaitForSeconds(0.25f);
        loadMenu.SetActive(false);
    }

    void Action_canceled()
    {
        UseButton();
    }
    
    void Bomb_canceled()
    {
        if(isActive)
        {
            isActive = false;
            FindObjectOfType<MainMenuController>().OpenMainMenu();
            CloseLoadMenu();
            Debug.Log("Le LoadMenu se ferme et laisse place au MainMenu");
        }
    }    

    public void UseButton()
    {
        if(isActive)
        {
            FindObjectOfType<SceneTransition>().LoadScene("SafePlace");
            //Set saveSlot settings;
        }
    }
}

