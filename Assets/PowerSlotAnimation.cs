using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlotAnimation : MonoBehaviour
{
    Animator animator;
    public string currentState;

    public string POWERSLOT_IDLE = "PowerSlot_Idle";
    public string POWERSLOT_ISTRIGGER = "PowerSlot_IsTrigger";

    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = POWERSLOT_IDLE;
    }

    public void ChangeAnimationState(string newState)
    {
        if(CanChangeAnimation(newState))
        {
            animator.Play(newState);
            currentState = newState;
        }
    }

    bool CanChangeAnimation(string newState)
    { 
        return true; // L'animation est interruptible
    }

        
    bool IsAnimationFinished(string animationName)
    {
        // Vérifiez si l'animation actuelle est celle que vous voulez
        if(animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            return true;
        }

        return false;
    }
}
