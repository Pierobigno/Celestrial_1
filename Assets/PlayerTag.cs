using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    public GameObject playerTag;
    public float tagCooldown;
    private bool timerOn;
    private float timer;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        TimerOn();
    }

    void Update()
    {
        if(timerOn && playerMovement.direction.x > 0)
        {
            timer += Time.deltaTime;
        }

        if(timer >= tagCooldown)
        {
            timer -= tagCooldown;
            DropTag();
        }
    }

    void TimerOn()
    {
        timerOn = true;
    }

    void DropTag()
    {
        Instantiate(playerTag, transform.position, transform.rotation);
    }
}
