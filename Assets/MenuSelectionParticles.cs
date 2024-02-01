using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectionParticles : MonoBehaviour
{   
    private float timer;
    private bool timerOn;
    public float maxSpeed;
    private float speed;

    void Start()
    {
        Destroy(gameObject, 2);
        timerOn = true;
    }

    void Update()
    {      
        if(timerOn)
        {
            speed = maxSpeed;
            transform.position += transform.right * Time.unscaledDeltaTime * speed;
            if(timer > 0.3f)
            {
                speed = 0;
                timerOn = false;
            }
        }
    }
}
