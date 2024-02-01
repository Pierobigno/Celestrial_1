using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SaveSlotManager : MonoBehaviour
{
    public GameObject saveSlotPrefab;
    public GameObject loadMenu;
    public List<GameObject> saveSlots = new List<GameObject>();
    public bool firstSave = true;

    void Update()
    {
        if(loadMenu == null && (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "FirstMainMenu"))
        {
            loadMenu = FindObjectOfType<LoadMenuController>().transform.GetChild(0).gameObject;
        }
    }

    public void AddSaveSlot(string name, string date, int currentPlayerLevel, int playerPowerSlots, float maxGameDistance, float maxGamePoints)
    {
        GameObject newSaveSlot = Instantiate(saveSlotPrefab, loadMenu.transform.GetChild(0));
        saveSlots.Add(newSaveSlot); // Ajouter le nouveau saveSlot à la liste saveSlots

        newSaveSlot.GetComponent<SaveSlot>().saveSlotName = name;
        newSaveSlot.GetComponent<SaveSlot>().saveSlotDate = date;
        newSaveSlot.GetComponent<SaveSlot>().saveSlotPlayerLevel = currentPlayerLevel;
        newSaveSlot.GetComponent<SaveSlot>().playerPowerSlots = playerPowerSlots;
        newSaveSlot.GetComponent<SaveSlot>().maxGameDistance = maxGameDistance;
        newSaveSlot.GetComponent<SaveSlot>().maxGamePoints = maxGamePoints;
    }

    public void RemoveSaveSlots()
    {
        foreach (GameObject saveSlot in saveSlots)
        {
            Destroy(saveSlot); // Détruire chaque saveSlot dans la liste saveSlots
        }
        saveSlots.Clear(); // Vider la liste saveSlots
    }

    public void SaveTheGame()
    {
        if(!firstSave)
        {
            AddSaveSlot(
            FindObjectOfType<VirtualKeyboardController>().validKeyboardWord.text,
            GetCurrentDate(),
            FindObjectOfType<GamePointsCalculator>().currentPlayerLevel,
            FindObjectOfType<PowerSlotsManager>().startingSlots,
            FindObjectOfType<DistanceCalculator>().maxDistance,
            FindObjectOfType<GamePointsCalculator>().maxGamePoints);

            FindObjectOfType<VirtualKeyboardController>().CloseVirtualKeyboard();

            FindObjectOfType<UiInfosBox>().OpenUiInfosBox("Partie sauvegardée!");
        }
        else
        {
            firstSave = false;
            AddSaveSlot(FindObjectOfType<VirtualKeyboardController>().validKeyboardWord.text, GetCurrentDate(), 1, 3, 0, 0);
            FindObjectOfType<VirtualKeyboardController>().CloseVirtualKeyboard();
            FindObjectOfType<UiInfosBox>().OpenUiInfosBox("Partie sauvegardée!");
        }
        
    }

    // Récupère automatiquement la date du système
    string GetCurrentDate()
    {
        DateTime currentDate = DateTime.Now;
        string formattedDate = currentDate.ToString("dd-MM-yyyy");
        return formattedDate;
    }
}
