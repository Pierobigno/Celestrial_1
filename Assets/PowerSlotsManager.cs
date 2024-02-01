using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PowerSlotsManager : MonoBehaviour
{
    public GameObject powerSlotPrefab;
    public int startingSlots = 0; // le nombre de powerSlots au début
    public int currentSlotCount = 0; // le nombre actuel de powerSlots

    public GameObject[] powerSlots;
    public List<GameObject> enablePowerSlots;
    public GameObject lastEnablePowerSlot;

    public int maxNumberOfSlots = 18;

    public bool isAbleToUsePowers;

    void Awake()
    {
        currentSlotCount = startingSlots;  
    }

    void Start()
    {
        enablePowerSlots = new List<GameObject>();
    }

    void Update()
    {
        // A optimiser pour éviter d'utiliser la méthode Update(). Voir sur chacun des pouvoirs (dash, shield, slowTime) si possible d'ajouter les deux fonctions.
        SetEnablePowerSlots();
        SetPowerSlots();

        if(powerSlots.Length < maxNumberOfSlots)
        {
            powerSlots = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                powerSlots[i] = transform.GetChild(i).gameObject;
            }    
        }    

        if(enablePowerSlots.Count <= 0)
        {
            isAbleToUsePowers = false;
        }
        else
        {
            isAbleToUsePowers = true;
        }
    }

    public void SetPowerSlots()
    {
        // Ajouter ou supprimer des powerSlots pour correspondre au nombre actuel
        int delta = currentSlotCount - transform.childCount;

        if (delta > 0)
        {
            for (int i = 0; i < delta; i++)
            {
                GameObject newPowerSlot = Instantiate(powerSlotPrefab, transform);
                powerSlots = new GameObject[transform.childCount]; // Mettre à jour le tableau powerSlots
                powerSlots[i] = newPowerSlot; // Ajouter le nouveau powerSlot au tableau powerSlots
            }
        }
        else if (delta < 0)
        {
            // Supprimer des powerSlots
            int removeCount = Mathf.Abs(delta);
            for (int i = 0; i < removeCount; i++)
            {
                GameObject powerSlotToRemove = powerSlots[i];
                powerSlots[i] = null; // Définir l'emplacement du powerSlot à null dans le tableau powerSlots
                Destroy(powerSlotToRemove); // Détruire le powerSlot
            }
        }
    }

    public void SetEnablePowerSlots()
    {
        // Réinitialise la liste
        enablePowerSlots.Clear();

        // Ajoute les slots non déclenchés, donc disponibles
        foreach(GameObject powerSlot in powerSlots)
        {
            if(!powerSlot.GetComponent<PowerSlot>().isTriggered)
            {
                enablePowerSlots.Add(powerSlot);
            }
        }
    }

    public void TriggerPowerSlots()
    {
        // Réinitialise la liste
        enablePowerSlots.Clear();

        // Ajoute les slots non déclenchés, donc disponibles
        foreach(GameObject powerSlot in powerSlots)
        {
            if(!powerSlot.GetComponent<PowerSlot>().isTriggered)
            {
                enablePowerSlots.Add(powerSlot);
            }
        }

        //Vérifie que le nombre de slots est > 0, désigne le dernier slot disponible, trouve son Animator et le déclenche dans PowerSlot et dans l'Animator
        if(enablePowerSlots.Count > 0)
        {
            lastEnablePowerSlot = enablePowerSlots[enablePowerSlots.Count - 1];
            lastEnablePowerSlot.GetComponent<PowerSlot>().isTriggered = true;

            PowerSlotAnimation powerSlotAnimation = lastEnablePowerSlot.GetComponent<PowerSlotAnimation>();
            powerSlotAnimation.ChangeAnimationState(powerSlotAnimation.POWERSLOT_ISTRIGGER);
        }
        else
        {
            Debug.Log("Le joueur n'a plus de slot de pouvoir disponible");
        }
    }

    // void ProvidePowerSlots sur la Prefab PowerSlot
}