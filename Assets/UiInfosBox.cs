using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiInfosBox : MonoBehaviour
{
    public bool isOpen;

    void Start()
    {
        PlayerInput input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();
    }

    public void OpenUiInfosBox(string wichText)
    {
        GetComponent<Animator>().SetBool("isOpen", true);
        isOpen = true;
        TextMeshProUGUI uiInfosBoxText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        uiInfosBoxText.text = wichText;
    }

    public void CloseUiInfosBox()
    {
        GetComponent<Animator>().SetBool("isOpen", false);
        isOpen = false;
    }
    
    void Action_canceled()
    {
        if(isOpen)
        {
            CloseUiInfosBox();
        }
    }
}
