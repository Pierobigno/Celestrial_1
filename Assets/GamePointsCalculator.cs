using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GamePointsCalculator : MonoBehaviour
{
    [Header("Game Points Manager")]
    public TextMeshProUGUI gamePointsText;
    public TextMeshProUGUI maxGamePointsText;
    public float currentGamePoints;
    public float maxGamePoints;
    public float bonusGamePoints;

    [Header("Level Manager")]
    public TextMeshProUGUI playerLevelText;
    public GameObject levelUpEffect;
    public int currentPlayerLevel;
    public float[] levelUpThresholds;
    private Transform player;

    [Header("InfosBox Display")]
    public string levelUpButton;
    public string levelUpText;

    [Header("Combo Manager")]
    public TextMeshProUGUI comboText;
    public float currentComboCoef;

    void Start()
    {
        currentGamePoints = 0 + bonusGamePoints;
        currentPlayerLevel = 1;
    }

    void Update()
    {
        gamePointsText.text = "Vous avez obtenu " + currentGamePoints.ToString() + " points";
        playerLevelText.text = "Vous êtes de niveau " + currentPlayerLevel.ToString();
        comboText.text = "Vous avez un bonus de combo de " + currentComboCoef.ToString() + "%";

        if(currentComboCoef > 0)
        {
            currentComboCoef -= Time.deltaTime;
        }

        if(currentGamePoints > maxGamePoints)
        {
            maxGamePoints = currentGamePoints;
        }
    }

    public void AddGamePoints(float gamePoints)
    {
        currentGamePoints += gamePoints * (1 + currentComboCoef);
        currentComboCoef += 0.5f;

        if (currentPlayerLevel < levelUpThresholds.Length && currentGamePoints > levelUpThresholds[currentPlayerLevel])
        {
            StartCoroutine(LevelUp());
        }
    }

    IEnumerator LevelUp()
    {
        //FindObjectOfType<AudioManager>().PlaySfx(0);
        currentPlayerLevel += 1;
        player = FindObjectOfType<PlayerMovement>().transform;
        Instantiate(levelUpEffect, player.position, player.rotation);
        InfosBox infosBox = FindObjectOfType<InfosBox>();
        infosBox.OpenGameInfosBox(levelUpButton, levelUpText);
        yield return new WaitForSeconds(3);
        infosBox.CloseGameInfosBox();
    }
}
