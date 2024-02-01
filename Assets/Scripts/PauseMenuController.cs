using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public GameObject pauseMenu;
    public GameObject[] pauseMenuButtons;
    public GameObject cursor;
    private Gamepad gamepad;
    private float timer;
    private bool timerOn;
    private float buttonTimer;
    private bool buttonTimerOn;
    public int targetButton;
    private Vector2 direction;
    private PlayerInput input;
    public string menuButton;

    public static bool gameIsPaused = false;
    public bool isOpen;

    public bool safePlaceAvailable;

    private void Awake()
    {
        gamepad = Gamepad.current;
    }
    
    private void Start()
    {
        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();
        input.Player.Pause.canceled += x => Pause_canceled();
    }

    void Update()
    {
        if(gamepad != null && isOpen)
        {
            direction = gamepad.leftStick.ReadValue() * -1;

            cursor.transform.position = pauseMenuButtons[targetButton].transform.position;

            if(targetButton < (pauseMenuButtons.Length - 1) && direction.y > 0 && !timerOn)
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

    void Pause_canceled()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "SafePlace")
        {
            if(gameIsPaused)
            {
                ResumeGame();
            }

            else
            {
                PauseGame();            
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        // pauseMenu.GetComponent<DissolveFadeModifier>().DissolveFadeOn();
        FindObjectOfType<TimeManager>().StopTime();
        Debug.Log("Le jeu est mis en pause");
        Animator pauseMenuAnimator = pauseMenu.GetComponent<Animator>();
        pauseMenuAnimator.SetBool("isOpen", true);
        InitializeCursorPosition();
        gameIsPaused = true;
        isOpen = true;
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeGameCoroutine());  
    }

    IEnumerator ResumeGameCoroutine()
    {
        // pauseMenu.GetComponent<DissolveFadeModifier>().DissolveFadeOff();
        FindObjectOfType<TimeManager>().ResumeTime();
        Debug.Log("Le joueur reprend le jeu");
        Animator pauseMenuAnimator = pauseMenu.GetComponent<Animator>();
        pauseMenuAnimator.SetBool("isOpen", false);
        yield return new WaitForSeconds(0.25f);
        ResetCursorPosition();
        gameIsPaused = false;
        isOpen = false;  
        pauseMenu.SetActive(false);
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
        ConfirmationWindow confirmationWindow = FindObjectOfType<ConfirmationWindow>();
        if(isOpen && !confirmationWindow.isOpen)
        {
            UseButton();
        }
    }

    public void UseButton()
    {
        ConfirmationWindow confirmationWindow = FindObjectOfType<ConfirmationWindow>();
        if(isOpen && !confirmationWindow.isOpen && !buttonTimerOn)
        {
            if(pauseMenuButtons[targetButton].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Continuer l'aventure")
            {
                ResumeGame();
                Debug.Log("Le jeu reprend");
            }

            else if(pauseMenuButtons[targetButton].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Rentrer au bercail")
            {
                if(safePlaceAvailable)
                {
                    Debug.Log("La boite de confirmation doit s'ouvrir");
                    confirmationWindow.OpenConfirmationWindow("Confirmation", "Es-tu sûr de vouloir rentrer au bercail", true, true, false);

                    string sceneToLoad = "SafePlace";
                    StartCoroutine(WaitForAnswer(sceneToLoad));
                }
                else
                {
                    Debug.Log("La boite de confirmation doit s'ouvrir");
                    confirmationWindow.OpenConfirmationWindow("Information", "Action impossible pour le moment", true, true, false);
                }
            }

            else
            {
                Debug.Log("La boite de confirmation doit s'ouvrir");
                confirmationWindow.OpenConfirmationWindow("Confirmation", pauseMenuButtons[targetButton].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, true, true, false);

                string sceneToLoad = "MainMenu";
                StartCoroutine(WaitForAnswer(sceneToLoad));
            }
        }
    }

    IEnumerator WaitForAnswer(string sceneToLoad)
    {
        ConfirmationWindow confirmationWindow = FindObjectOfType<ConfirmationWindow>();
        while(confirmationWindow.answer == ""){yield return null;}
        {
            if(confirmationWindow.answer == "Accept")
            {
                ResumeGame();
                yield return new WaitForSeconds(0.2f); //Attend la fermeture du menu de pause
                FindObjectOfType<SceneTransition>().LoadScene(sceneToLoad);
                Debug.Log("Le joueur retourne à " + sceneToLoad);
            }
            else
            {
                //
            }
        }            
    }
}

