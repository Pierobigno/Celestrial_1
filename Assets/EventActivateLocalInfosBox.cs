using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActivateLocalInfosBox : MonoBehaviour
{
    public GameObject localInfosBox;
    public bool activateIfIsAble;
    public bool activateIfIsTriggered;

    void Update()
    {
        if(activateIfIsAble && GetComponent<EventStates>().isAble)
        {
            localInfosBox.GetComponent<LocalInfosBox>().OpenLocalInfosBox();
        }

        else if(activateIfIsTriggered && GetComponent<EventStates>().isTriggered)
        {
            localInfosBox.GetComponent<LocalInfosBox>().OpenLocalInfosBox();
        }
    }
}
