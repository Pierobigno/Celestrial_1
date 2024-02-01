using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Classe pour représenter un élément de l'inventaire
[System.Serializable]
public class EquippedItem
{
    public string name;
    public Sprite itemSprite;
    public string itemDescription;
    public bool isAddedToAList;

    public EquippedItem(string name, Sprite itemSprite, string itemDescription)
    {
        this.name = name;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
    }
}