using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class VirtualKeyboardController : MonoBehaviour
{
    public bool vkIsOpen;
    public GridLayoutGroup gridLayout;
    public GameObject[] virtualKeyboardButtons;
    public GameObject cursor;
    public int targetButton;
    public Vector2 direction;
    public string currentInput;
    public TextMeshProUGUI currentKeyboardWord;
    public TextMeshProUGUI validKeyboardWord;
    public bool vkWordValidated;

    [HideInInspector] public string selectedItemName;

    private Gamepad gamepad;
    private float timer;
    private bool timerOn;
    private VKBBehaviour currentInputBehaviour;
    private PlayerInput input;

    void Awake()
    {
        gamepad = Gamepad.current;
        CloseVirtualKeyboard();
    }

    void Start()
    {
        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();
        input.Player.Bomb.canceled += x => Bomb_canceled();
    }

    void Update()
    {
        if (gamepad != null)
        {
            direction = gamepad.leftStick.ReadValue();

            cursor.transform.position = virtualKeyboardButtons[targetButton].transform.position;

            if (targetButton + 1 < virtualKeyboardButtons.Length && direction.x > 0.5f && !timerOn)
            {
                targetButton += 1;
                timerOn = true;
            }

            if (targetButton - 1 >= 0 && direction.x < -0.5f && !timerOn)
            {
                targetButton -= 1;
                timerOn = true;
            }

            if (targetButton - 13 >= 0 && direction.y > 0.5f && !timerOn)
            {
                targetButton -= 13;
                timerOn = true;
            }

            if (targetButton + 13 < virtualKeyboardButtons.Length && direction.y < -0.5f && !timerOn)
            {
                targetButton += 13;
                timerOn = true;
            }
        }

        if (timerOn)
        {
            timer += Time.unscaledDeltaTime;
            if (timer > 0.2f)
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

    void Action_canceled()
    {
        UseKBButton();
    }

    public void UseKBButton()
    {
        if(vkIsOpen)
        {
            currentInput = virtualKeyboardButtons[targetButton].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            currentInputBehaviour = virtualKeyboardButtons[targetButton].GetComponent<VKBBehaviour>();

            if(currentInputBehaviour.isAddButton)
            {
                currentKeyboardWord.text += currentInput;
            }
            else if (currentInputBehaviour.isValidationButton)
            {
                ValidateTheWord();
            }
            else
            {
                currentKeyboardWord.text = currentKeyboardWord.text.Remove(currentKeyboardWord.text.Length - 1);
            }
        }
    }

    List<Transform> GetChildren()
    {
        Transform parentTransform = transform;
        int childrenCount = parentTransform.childCount;

        // Créer une liste pour stocker les enfants
        List<Transform> childrenList = new List<Transform>();

        for (int i = 0; i < childrenCount; i++)
        {
            // Accéder à chaque enfant individuellement
            Transform child = parentTransform.GetChild(i);
            childrenList.Add(child); // Ajouter l'enfant à la liste
        }

        // Retourner la liste des enfants si vous en avez besoin en dehors de cette fonction
        return childrenList;
    }

    public void CloseVirtualKeyboard()
    {
        Debug.Log("Le clavier virtuel se ferme");
        vkIsOpen = false;

        List<Transform> childrenList = GetChildren();

        // Parcourir la liste pour désactiver chaque enfant
        foreach (Transform child in childrenList)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OpenVirtualKeyboard()
    {
        StartCoroutine(OpenVirtualKeyboardCoroutine());
    }

    IEnumerator OpenVirtualKeyboardCoroutine()
    {
        Debug.Log("Le clavier virtuel s'ouvre");
        vkIsOpen = true;

        yield return new WaitForSeconds(0.1f);

        ResetValidWord();

        List<Transform> childrenList = GetChildren();

        // Parcourir la liste pour désactiver chaque enfant
        foreach (Transform child in childrenList)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void ValidateTheWord()
    {
        vkWordValidated = true;
        validKeyboardWord.text = currentKeyboardWord.text;
        CloseVirtualKeyboard();
    }

    public void ResetValidWord()
    {
        vkWordValidated = false;
        currentKeyboardWord.text = "";
        validKeyboardWord.text = "";
    }

    void Bomb_canceled()
    {
        if(vkIsOpen)
        {
            CloseVirtualKeyboard();
            ResetValidWord();
            StopAllCoroutines();
        }      
    }    
}
