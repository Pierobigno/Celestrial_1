using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Gamepad gamepad;
    private float timer;
    private bool timerOn;
    private float buttonTimer;
    private bool buttonTimerOn;
    private Vector2 direction;
    private PlayerInput input;
    public bool isActive = true;

    public GridLayoutGroup gridLayout;
    public GameObject mainMenu;
    public string sceneToLoadToContinue;
    public GameObject[] mainMenuButtons;
    public GameObject cursor;
    public GameObject selectionParticles;

    [Header("Generalités")]
    public int targetButton;

    void OnEnable()
    {
        gamepad = Gamepad.current;
        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();

        OpenMainMenu();
        Debug.Log("OnEnable rouvre le MainMenu");
    }

    void Update()
    {
        if(gamepad != null && isActive)
        {
            direction = gamepad.leftStick.ReadValue() * -1;

            cursor.transform.position = mainMenuButtons[targetButton].transform.position;

            if(targetButton < (mainMenuButtons.Length - 1) && direction.y > 0 && !timerOn)
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

        if(buttonTimerOn)
        {
            buttonTimer += Time.unscaledDeltaTime;
            if(buttonTimer > 0.5f)
            {
                buttonTimer = 0;
                buttonTimerOn = false;
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

    void Action_canceled()
    {
        UseButton();
    }

    public void UseButton()
    {
        ConfirmationWindow confirmationWindow = FindObjectOfType<ConfirmationWindow>();
        Instantiate(selectionParticles, mainMenuButtons[targetButton].transform.position, mainMenuButtons[targetButton].transform.rotation);
        if(isActive && !confirmationWindow.isOpen && !buttonTimerOn)
        {
            buttonTimerOn = true;
            
            if(mainMenuButtons[targetButton].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Continuer l'aventure")
            {
                if(FindObjectOfType<SaveSlotManager>().saveSlots.Count > 0)
                {
                    string sceneToLoad = sceneToLoadToContinue;
                    StartCoroutine(LaunchScene(sceneToLoad));
                }
                else
                {
                    StartCoroutine(ImpossibleActionCoroutine());
                }
                
            }

            else if(mainMenuButtons[targetButton].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Charger une partie")
            {
                if(FindObjectOfType<SaveSlotManager>().saveSlots.Count > 0)
                {
                    CloseMainMenu();
                    FindObjectOfType<LoadMenuController>().OpenLoadMenu();
                }
                else
                {
                    StartCoroutine(ImpossibleActionCoroutine());
                }
            }
            else if(mainMenuButtons[targetButton].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Nouvelle partie")
            {
                StartCoroutine(NewGameCreation());
            }
            else
            {
                Application.Quit();
                Debug.Log("Quitter le jeu");
            }
        }
    }

    IEnumerator ImpossibleActionCoroutine()
    {
        ConfirmationWindow confirmationWindow = FindObjectOfType<ConfirmationWindow>();
        confirmationWindow.OpenConfirmationWindow("Information", "Action impossible tant que vous n'avez pas créé de nouvelle partie", true, true, false);
        CloseMainMenu();
        while(confirmationWindow.answer == ""){yield return null;}
        OpenMainMenu();
    }

    IEnumerator LaunchScene(string sceneToLoad)
    {
        CinemachineController cinemachineController = FindObjectOfType<CinemachineController>();
        cinemachineController.ChangeCameraTarget(mainMenuButtons[targetButton].transform);
        yield return new WaitForSeconds(0.5f);
        cinemachineController.CameraZoom();
        yield return new WaitForSeconds(1);
        cinemachineController.CameraFix();        
        yield return new WaitForSeconds(1);
        FindObjectOfType<SceneTransition>().LoadScene(sceneToLoad);
    }

    IEnumerator NewGameCreation()
    {
        VirtualKeyboardController vkController = FindObjectOfType<VirtualKeyboardController>();
        SaveSlotManager saveSlotManager = FindObjectOfType<SaveSlotManager>();
        UiInfosBox uiInfosBox = FindObjectOfType<UiInfosBox>();

        Debug.Log("Le joueur souhaite commencer une nouvelle partie ce qui ouvre le clavier et desactive MainMenu");
        CloseMainMenu();
        vkController.OpenVirtualKeyboard();
        mainMenu.SetActive(false);
        saveSlotManager.firstSave = true;

        yield return new WaitForSeconds(0.1f);

        while(vkController.vkIsOpen)
        {
            Debug.Log("Attend la fermeture du clavier virtuel");
            yield return null;
        }

        if(vkController.vkWordValidated)
        {
            Debug.Log("Le joueur crée un slot de sauvegarde " + vkController.validKeyboardWord + " dans le SaveSlotManager");
            saveSlotManager.SaveTheGame();

            yield return new WaitForSeconds(0.1f);

            while(uiInfosBox.isOpen)
            {
                Debug.Log("Attend la fermeture de la boîte d'information de l'UI");
                yield return null;
            }

            SceneManager.LoadScene("Level1");
        }
        else
        {
            Debug.Log("Le joueur annule son action");
            OpenMainMenu();
            saveSlotManager.firstSave = false;
            mainMenu.SetActive(true);
        }
    }

    public void OpenMainMenu()
    {
        isActive = true;
        mainMenu.SetActive(true);
        Animator mainMenuAnimator = mainMenu.GetComponent<Animator>();
        mainMenuAnimator.SetBool("isOpen", true);
    }

    public void CloseMainMenu()
    {
        StartCoroutine(CloseMainMenuCoroutine());
    }

    IEnumerator CloseMainMenuCoroutine()
    {
        isActive = false;
        Animator mainMenuAnimator = mainMenu.GetComponent<Animator>();
        mainMenuAnimator.SetBool("isOpen", false);
        yield return new WaitForSeconds(0.25f);
        mainMenu.SetActive(false);
    }
}

