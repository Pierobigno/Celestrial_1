using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class TimeManager : MonoBehaviour
{
    private float timer;
    private float originalTimeScale;
    private float timeSlowedTimer = 0f;

    public float slowTimeScale = 0.1f;
    public float slowdownDuration = 2f;
    public float stopTimeDuration;

    public bool isTimeSlowed = false;
    public bool timeStopped;

    public void StopTime()
    {
        timeStopped = true;
    }

    public void ResumeTime()
    {
        timeStopped = false;
        Time.timeScale = originalTimeScale; // Rétablit le TimeScale à sa valeur d'origine
    }

    public void SlowTimeEffect(float slowTimeScale, float duration)
    {
        Debug.Log("Fonction SlowTimeEffect() déclenchée");
        if(!isTimeSlowed)
        {
            isTimeSlowed = true;
            slowdownDuration = duration;
            originalTimeScale = Time.timeScale;
            Time.timeScale = slowTimeScale;
            timeSlowedTimer = 0f;
        }
        else
        {
            return;
        }
    }

    void Update()
    {
        // Ralentissement du temps
        if(isTimeSlowed)
        {
            timeSlowedTimer += Time.unscaledDeltaTime;

            if(timeSlowedTimer >= slowdownDuration)
            {
                isTimeSlowed = false;
                Time.timeScale = originalTimeScale;
            }
        }

        // Arrêt du temps
        else if(timeStopped)
        {
            Time.timeScale = 0f; // Arrête le temps en mettant TimeScale à zéro

            // if(!FindObjectOfType<Inventory>().inventoryIsOpen) // Si l'arrêt du temps n'est pas déclenché par l'ouverture de l'inventaire
            // {
                timer += Time.unscaledDeltaTime;
                if(timer > stopTimeDuration)
                {
                    ResumeTime();
                    timer = 0;
                }
            // }
        }

        else
        {
            Time.timeScale = 1f;
        }
    }
}
