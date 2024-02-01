using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShakeEZ : MonoBehaviour
{   
    public float delay = 0f;
    public float magn = 1f;
    public float rough = 1f;
    public float fadeIn = 0.1f;
    public float fadeOut = 0.1f;

    private bool timerOn = true;
    private float timer;

    void Update()
    {
        if(timerOn)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timerOn = false;
                timer = 0;
                ShakeCamera();
            }
        }
    }

    void ShakeCamera()
    {
        CameraShaker.Instance.ShakeOnce(magn, rough, fadeIn, fadeOut);
    }
}
