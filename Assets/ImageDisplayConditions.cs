using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ImageDisplayConditions : MonoBehaviour
{
    public bool isActive;
    public bool hideIfDialogueIsOpen;
    public bool hideAtStart;
    public string[] sceneNameCondition;
    public Image[] images;

    void Start()
    {
        if(hideAtStart)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (Array.Exists(sceneNameCondition, condition => condition == sceneName))
            {
                HideImage();
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
                foreach(Image image in images)
                {
                    image.enabled = false;
                }
            }
            if(!FindObjectOfType<DialogueManager>().dialogueIsOpen && !isActive)
            {
                isActive = true;
                foreach(Image image in images)
                {
                    image.enabled = true;
                }
            }
        }
    }

    void HideImage()
    {
        foreach(Image image in images)
        {
            image.enabled = false;
        }
    }
}
