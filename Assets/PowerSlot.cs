using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlot : MonoBehaviour
{
    public bool isTriggered;

    private bool timerOn;
    private float timer;
    public float powerSlotCooldown;

    void Update()
    {
        if(isTriggered)
        {
            timerOn = true;
        }

        if(timerOn)
        {
            timer += Time.deltaTime;
            if(timer > powerSlotCooldown)
            {
                timerOn = false;
                timer = 0;
                ProvidePowerSlot();
            }
        }
        else
        {
            timer = 0;
        }
    }

    public void ProvidePowerSlot()
    {
        isTriggered = false;
        PowerSlotAnimation powerSlotAnimation = GetComponent<PowerSlotAnimation>();
        powerSlotAnimation.ChangeAnimationState(powerSlotAnimation.POWERSLOT_IDLE);
    }        
}
