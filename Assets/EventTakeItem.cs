using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTakeItem : MonoBehaviour
{
    void Update()
    {
        if(GetComponent<EventStates>().isTriggered)
        {
            FindObjectOfType<Inventory>().AddAvailableItem(gameObject.name, GetComponent<SpriteRenderer>().sprite, GetComponent<ItemDescription>().itemDescription);
            Debug.Log("Ajoute l'item " + name + " à la liste des items disponibles");
            Destroy(gameObject);
        }
    }
}
