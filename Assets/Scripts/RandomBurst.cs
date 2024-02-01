using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBurst : MonoBehaviour
{
    private Vector2 initialPosition;
    private Vector3 randomDirection;
    private Quaternion initialRotation;
    private float randomRotation;
    private float elapsedTime = 0f;
    public bool isBursting;
    public bool isReturning;

    public float burstSpeed;
    public float rotationSpeed;
    public float returnDelay;
    public Collider2D[] colliders;

    void Start()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        initialPosition = transform.GetChild(0).position;
        initialRotation = transform.GetChild(0).rotation;
        randomRotation = Random.Range(-1f, 1f);
        Burst();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(!other.isTrigger)
            {
                UnBurst();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(!other.isTrigger)
            {
                Burst();
            }
        }
    }

    void Burst()
    {
        isBursting = true;
        isReturning = false;
        colliders = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    void UnBurst()
    {
        isReturning = true;
        isBursting = false;
        colliders = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
    }

    void DontMove()
    {
        transform.GetChild(0).position = initialPosition;
        transform.GetChild(0).rotation = initialRotation;
        isReturning = false;
        isBursting = false;
    }

    void Update()
    {
        if(isBursting)
        {
            transform.GetChild(0).position += (Vector3)randomDirection * burstSpeed * Time.deltaTime;
            transform.GetChild(0).rotation *= Quaternion.Euler(0f, 0f, randomRotation * rotationSpeed * Time.deltaTime);
        }

        else if(isReturning)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= returnDelay)
            {
                elapsedTime = 0f;
                DontMove();
            }

            transform.GetChild(0).position = Vector2.Lerp(transform.GetChild(0).position, initialPosition, elapsedTime / returnDelay);
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, initialRotation, elapsedTime / returnDelay);
            if(Vector2.Distance(transform.GetChild(0).position, initialPosition) < 0.01f)
            {
                transform.GetChild(0).position = initialPosition;
                transform.GetChild(0).rotation = initialRotation;
                randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            }
        }
    }
}