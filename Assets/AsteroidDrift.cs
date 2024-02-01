using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDrift : MonoBehaviour
{
    [Header("Gestion de la dérive de l'asteroïde")]
    public float moveSpeed;
    public float rotationSpeed;
    public bool isMovingAtStart;
    private bool isMoving;

    private Vector2 initialPosition;
    private Vector3 randomDirection;
    private Quaternion initialRotation;
    private float randomRotation;

    void Start()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        randomRotation = Random.Range(-1f, 1f);

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
            transform.position += (Vector3)randomDirection * moveSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0f, 0f, randomRotation * rotationSpeed * Time.deltaTime);
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
