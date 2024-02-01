using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalCountdown : MonoBehaviour
{
    public float finalCountdownTimer;
    public TextMeshProUGUI finalCountdownText;
    private float roundedFinalCountdownTimer;
    public GameObject player;
    public string sceneToLoad;

    void Start()
    {
        finalCountdownTimer = 5;
    }

    void Update()
    {
        if(player != null)
        {
            finalCountdownTimer -= Time.deltaTime;
            roundedFinalCountdownTimer = Mathf.Round(finalCountdownTimer);
            finalCountdownText.text = "Sortez de la zone dans les " + roundedFinalCountdownTimer.ToString() + " secondes avant d'être réduit en poussière";

            if(roundedFinalCountdownTimer <= 0)
            {
                Destroy(player);
                finalCountdownText.text = "Quel gâchis...";
                FindObjectOfType<SceneTransition>().LoadScene(sceneToLoad);
            }
        }
    }
}
