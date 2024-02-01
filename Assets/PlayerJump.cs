using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJump : MonoBehaviour
{
    public GameObject explosionJump;
    SpriteRenderer[] spriteRenderers;
    public Transform jumpTarget;
    private PlayerInput input;
    public bool jumpTargetOn;

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        input = new PlayerInput();
        if(SceneManager.GetActiveScene().name != "SafePlace")
        {
            input.Player.Enable();
        }
        else
        {
            input.Player.Disable();
        } 
        input.Player.Jump.performed += x => Jump_performed();
        input.Player.Jump.canceled += x => Jump_canceled();
    }

    void Jump_performed()
    {
        JumpTargetOn();
    }

    void Jump_canceled()
    {
        JumpTargetOff();
    }

    void JumpTargetOn()
    {
        jumpTargetOn = true;
        jumpTarget.position = transform.position;
        jumpTarget.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    void JumpTargetOff()
    {
        jumpTargetOn = false;
        transform.position = jumpTarget.position;
        jumpTarget.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(explosionJump, jumpTarget.position, jumpTarget.rotation);
    }

    void DisableSpriteRenderers()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        } 
    }

    void EnableSpriteRenderers()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = true;
        } 
    }
}
