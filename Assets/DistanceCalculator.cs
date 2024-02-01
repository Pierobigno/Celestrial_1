using System;
using UnityEngine;
using TMPro;

public class DistanceCalculator : MonoBehaviour
{
    private Vector2 initialPosition;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI maxDistanceText;
    public float maxDistance;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float distanceX = Mathf.Round(Mathf.Abs(transform.position.x - initialPosition.x));
        distanceText.text = "Vous avez parcouru " + distanceX.ToString() + " mètres de distance";

        if(distanceX > maxDistance)
        {
            maxDistance = distanceX;
        }
    }
}
