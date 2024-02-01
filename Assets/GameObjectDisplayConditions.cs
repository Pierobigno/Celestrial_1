using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameObjectDisplayConditions : MonoBehaviour
{
    public bool hideAtStartFor1;
    public bool hideAtStartFor2;
    public string[] sceneNameCondition1;
    public string[] sceneNameCondition2;
    public GameObject[] gos1;
    public GameObject[] gos2;

    void OnEnable()
    {
        if(hideAtStartFor1)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (Array.Exists(sceneNameCondition1, condition => condition == sceneName))
            {
                HideGameObject1();
            }            
        }

        if(hideAtStartFor2)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (Array.Exists(sceneNameCondition2, condition => condition == sceneName))
            {
                HideGameObject2();
            }            
        }
    }

    void HideGameObject1()
    {
        foreach(GameObject go1 in gos1)
        {
            go1.SetActive(false);
        }
    }

    void HideGameObject2()
    {
        foreach(GameObject go2 in gos2)
        {
            go2.SetActive(false);
        }
    }
}
