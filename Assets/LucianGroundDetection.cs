using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucianGroundDetection : MonoBehaviour
{
    private PolygonCollider2D[] colliders;
    public LayerMask groundLayer;
    public bool isGrounded;

    private float timer;
    public Transform groundPoint;
    public GameObject landingPrefab;

    void Start()
    {
        colliders = GetComponentsInChildren<PolygonCollider2D>();
    }

    void Update()
    {
        foreach(PolygonCollider2D collider in colliders)
        {
            if(collider.IsTouchingLayers(groundLayer))
            {
                isGrounded = true;
                GetComponent<Animator>().SetBool("isGrounded", true);
                timer = 0;
                break;
            }

            else
            {
                timer += Time.deltaTime;
                if(timer > 1f)
                {
                    isGrounded = false;
                    GetComponent<Animator>().SetBool("isGrounded", false);
                    timer = 0;
                }
            }
        }
    }

    void InstantiateLandingEffect()
    {
        Instantiate(landingPrefab, groundPoint.position, groundPoint.rotation);
    }
}
