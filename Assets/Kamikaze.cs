using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [Header("Gestion de la dérive du Kamikaze")]
    public float driftMoveSpeed;
    public float rotationSpeed;
    public bool isMovingAtStart;
    private bool isMoving;


    [Header("Gestion de l'attaque du Kamikaze")]
    public float distanceFromPlayer;
    public float attackMoveSpeed;
    public float distanceToAttack;
    public bool isChasingPlayer;

    private Vector2 initialPosition;
    private Vector3 randomDirection;
    private Quaternion initialRotation;
    private float randomRotation;
    private Transform player;

    void Start()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        randomRotation = Random.Range(-1f, 1f);
        player = FindObjectOfType<PlayerMovement>().transform;

        if(isMovingAtStart)
        {
            Move();
        }
        else
        {
            Stop();
        }
    }

    void Update()
    {
        if(isMoving)
        {
            transform.position += (Vector3)randomDirection * driftMoveSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0f, 0f, randomRotation * rotationSpeed * Time.deltaTime);
        }

        distanceFromPlayer = Mathf.Round(Vector3.Distance(player.position, transform.position));
        if(distanceFromPlayer < distanceToAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, attackMoveSpeed * Time.deltaTime);
            if(!isChasingPlayer)
            {
                isChasingPlayer = true;
            }            
        }
        else
        {
            if(isChasingPlayer)
            {
                isChasingPlayer = false;
            }
        }
    }

    void Move()
    {
        isMoving = true;
    }

    void Stop()
    {
        isMoving = false;
    }
}
