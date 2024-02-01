using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VKBBehaviour : MonoBehaviour
{
    public Transform virtualKeyboardCursor;
    // private Animator animator;
    public bool isActive;
    public bool isAddButton;
    public bool isValidationButton;

    void Start()
    {
        // animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(virtualKeyboardCursor.position == transform.position)
        {
            if(!isActive)
            {
                isActive = true;
                // animator.SetBool("isActive", true);
            }
        }

        else
        {
            isActive = false;
            // animator.SetBool("isActive", false);
        }
    }
}
