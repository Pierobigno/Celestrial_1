using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 startRotation;
    public Vector3 rotationOnTime;

    public bool rotateOn;

    public bool hasLimit;
    public Vector3 rotationLimit;
    public bool isRotatingClockwise;

    void Start()
    {
        transform.Rotate(startRotation); // Valeurs indiquees dans l'Inspector
        OnRotate();
    }

    void Update()
    {
        if(rotateOn)
        {
            if(hasLimit)
            {
                if(isRotatingClockwise)
                {
                    transform.Rotate(rotationOnTime * Time.deltaTime);
                }
                else
                {
                    transform.Rotate((-1) * rotationOnTime * Time.deltaTime);
                }

                if(transform.rotation.eulerAngles.z >= rotationLimit.z && transform.rotation.eulerAngles.z <= rotationLimit.z + 1f)
                {
                    isRotatingClockwise = !isRotatingClockwise;
                }
                else if(transform.rotation.eulerAngles.z <= 360f - rotationLimit.z && transform.rotation.eulerAngles.z >= 360f - (rotationLimit.z + 1f))
                {
                    isRotatingClockwise = !isRotatingClockwise;
                }
            }
            else
            {
                transform.Rotate(rotationOnTime * Time.deltaTime);
            }
        }
    }

    public void OnRotate()
    {
        rotateOn = !rotateOn;
    }
}


