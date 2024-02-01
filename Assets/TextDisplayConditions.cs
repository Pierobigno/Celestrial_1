using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class TextDisplayConditions : MonoBehaviour
{
    public bool isActive;
    public bool hideIfDialogueIsOpen;
    public bool hideAtStart;
    public string[] sceneNameCondition;
    public TextMeshProUGUI[] texts;

    void OnEnable()
    {
        if(hideAtStart)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (Array.Exists(sceneNameCondition, condition => condition == sceneName))
            {
                HideText();
            }            
        }
    }
    
    void Update()
    {
        if(hideIfDialogueIsOpen)
        {
            if(FindObjectOfType<DialogueManager>().dialogueIsOpen && isActive)
            {
                isActive = false;
                foreach(TextMeshProUGUI text in texts)
                {
                    text.enabled = false;
                }
            }
            if(!FindObjectOfType<DialogueManager>().dialogueIsOpen && !isActive)
            {
                isActive = true;
                foreach(TextMeshProUGUI text in texts)
                {
                    text.enabled = true;
                }
            }
        }
    }

    void HideText()
    {
        foreach(TextMeshProUGUI text in texts)
        {
            text.enabled = false;
        }
    }
}
