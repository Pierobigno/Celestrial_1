using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceAndLevelsManager : MonoBehaviour
{
    public GameObject powerUp;
    public float currentExperience;
    public float xpStep;
    public float currentLevel;

    void Start()
    {
        currentLevel = 1;
    }

    void Update()
    {   
        if(xpStep >= (100 * currentLevel))
        {
            StartCoroutine(LevelUp());
        }
    }

    IEnumerator LevelUp()
    {
        currentLevel +=1;
        powerUp.SetActive(true);
        xpStep = 0;
        //FindObjectOfType<AudioManager>().PlaySfx(0);
        yield return new WaitForSeconds (2);
        powerUp.SetActive(false);
    }

    public void AddXP(int xpProvided)
    {
        currentExperience += xpProvided;
        xpStep += xpProvided;
    }
}
