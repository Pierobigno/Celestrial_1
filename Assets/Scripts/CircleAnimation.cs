using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAnimation : MonoBehaviour
{
    public float radiusX = 2.0f;
    public float radiusY = 1.0f;
    public float speed = 1.0f;
    private float angle = 0.0f;

    public Vector3 offset;

    void Update()
    {
        float x = radiusX * Mathf.Cos(angle);
        float y = radiusY * Mathf.Sin(angle);

        transform.localPosition = new Vector3(x + offset.x, y + offset.y, 0 + offset.z);

        angle += speed * Time.deltaTime;

        if (angle >= 360.0f)
        {
            angle = 0.0f;
        }
    }
}
