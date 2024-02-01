using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointMovement : MonoBehaviour
{
    public float cameraFlySpeed;
    public bool isMoving;


    void Update()
    {
        if(isMoving)
        {
            transform.position += transform.right * cameraFlySpeed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position;
        }
    }
}
