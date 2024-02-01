using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfLifeDistanceText : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    private EndOfLifeMovement endOfLifeMovement;

    void Awake()
    {
        endOfLifeMovement = FindObjectOfType<EndOfLifeMovement>();
    }

    void Update()
    {
        distanceText.text = endOfLifeMovement.distanceText;
    }
}
