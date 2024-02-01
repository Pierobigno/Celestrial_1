using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    public GameObject warningMessage;
    public LayerMask playerLayer;
    public GameObject compass;
    public GameObject finalCountdownPrefab;
    public float maxTimeOutBeforeCD;
    private float timer;
    private bool finalCountdownOn;
    private bool isOut;

    void Start()
    {
        CinemachineController vCam = FindObjectOfType<CinemachineController>();
        vCam.currentTarget = transform;
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if(((1 << player.gameObject.layer) & playerLayer) != 0)
        {
            warningMessage.SetActive(false);
            compass.SetActive(false);
            finalCountdownPrefab.SetActive(false);
            timer = 0;
            isOut = false;
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        if(((1 << player.gameObject.layer) & playerLayer) != 0)
        {
            warningMessage.SetActive(true);
            compass.SetActive(true);
            finalCountdownPrefab.SetActive(true);
            finalCountdownPrefab.GetComponent<FinalCountdown>().finalCountdownTimer = 5;
            timer = 0;
            isOut = true;
        }
    }

    void Update()
    {
        if(isOut)
        {
            timer += Time.deltaTime;
            if(timer > maxTimeOutBeforeCD && !finalCountdownOn)
            {
                timer = 0;
                finalCountdownOn = true;
                finalCountdownPrefab.SetActive(true);
            }
        }
    } 
}