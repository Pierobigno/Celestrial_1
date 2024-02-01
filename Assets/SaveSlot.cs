using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    [Header("SaveSlot Settings")]
    public string saveSlotName;
    public TextMeshProUGUI name;
    public string saveSlotDate;
    public TextMeshProUGUI date;
    
    [Header("SaveSlot Player")]
    public int saveSlotPlayerLevel;
    public TextMeshProUGUI level;
    public int playerPowerSlots;
    public GameObject[] playerItems;

    [Header("SaveSlot Game")]
    public float maxGameDistance;
    public float maxGamePoints;

    void Start()
    {
        name.text = saveSlotName;
        date.text = saveSlotDate;
    }
}
