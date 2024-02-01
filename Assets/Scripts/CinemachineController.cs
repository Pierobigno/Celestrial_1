using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public Transform playerCameraPoint;
    private Transform cmPos;

    [Header("Zoom")]
    public bool camIsLocked;
    public bool cameraZoom;
    public bool cameraDezoom;
    public float zoomSpeed = 100f;

    [Header("Target")]
    public Transform currentTarget;

    void Start()
    {
        currentTarget = playerCameraPoint;
    }

    void Update()
    { 
        vcam.Follow = currentTarget;

        if(cameraZoom)
        {
            if(!camIsLocked)
            {
                vcam.m_Lens.FieldOfView -= (zoomSpeed * Time.deltaTime);
            }
        }
        
        if(cameraDezoom)
        {
            if(!camIsLocked)
            {
                vcam.m_Lens.FieldOfView += (zoomSpeed * Time.deltaTime);
            }
        }
    }

    public void CameraZoom()
    {
        cameraZoom = true;
        cameraDezoom = false;
    }

    public void CameraDezoom()
    {
        cameraZoom = false;
        cameraDezoom = true;
    }
    public void CameraFix()
    {
        cameraZoom = false;
        cameraDezoom = false;
    }

    public void ChangeCameraTarget(Transform newTarget)
    {
        if(currentTarget == playerCameraPoint)
        {
            currentTarget = newTarget;
        }
        else
        {
            currentTarget = playerCameraPoint;
        }
    }
}