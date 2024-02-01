using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInventory : MonoBehaviour
{
    public bool eventOpenInventory = true;

    void Update()
    {
        if(GetComponent<EventStates>().isTriggered)
        {
            if(!FindObjectOfType<Inventory>().isOpen)
            {
                FindObjectOfType<Inventory>().OpenInventory();
            }
        }
    }
}
