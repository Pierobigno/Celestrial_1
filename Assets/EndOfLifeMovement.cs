using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLifeMovement : MonoBehaviour
{
    public float speed;
    public bool isMoving;
    public float distanceFromPlayer;
    public string distanceText;
    private Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if(isMoving)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position;
        }

        distanceFromPlayer = Mathf.Round(Mathf.Abs(player.position.x - transform.position.x));
        distanceText = "Vous êtes à " + distanceFromPlayer.ToString() + " mètres de la Mort";
    }
}
