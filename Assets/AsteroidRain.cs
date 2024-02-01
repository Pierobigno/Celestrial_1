using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRain : MonoBehaviour
{
    [Header("Gestion de la vitesse, de la rotation et de l'angle de l'asteroïde")]
    public float moveSpeed;
    public float rotationSpeed;
    public bool isMovingAtStart;
    private bool isMoving;

    private Vector2 initialPosition;
    public Vector3 direction;
    private Quaternion initialRotation;
    private float randomRotation;

    void Start()
    {
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
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
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
