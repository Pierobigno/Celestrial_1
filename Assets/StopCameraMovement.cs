using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCameraMovement : MonoBehaviour
{
    private CameraPointMovement cameraPointMovement;
    private EventStates eventStates;

    public float timeBeforeStop;
    public float timeBeforeRelease;

    void Start()
    {
        cameraPointMovement = FindObjectOfType<CameraPointMovement>();
        eventStates = GetComponent<EventStates>();
    }

    void Update()
    {
        if(eventStates.isTriggered)
        {
            StartCoroutine(StopCamera());
        }
        if(eventStates.isOver)
        {
            StartCoroutine(ReleaseCamera());
        }
    }

    IEnumerator StopCamera()
    {
        yield return new WaitForSeconds(timeBeforeStop);
        cameraPointMovement.isMoving = false;
    }

    IEnumerator ReleaseCamera()
    {
        yield return new WaitForSeconds(timeBeforeRelease);
        cameraPointMovement.isMoving = true;
    }
}
