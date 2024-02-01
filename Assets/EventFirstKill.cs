using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFirstKill : MonoBehaviour
{
    public bool isActivate;

    void Update()
    {
        if(FindObjectOfType<EnemiesDeathCount>().currentEnemyDeathCount > 0 && !isActivate)
        {
            isActivate = true;
            GetComponent<EventStates>().isOver = true;
        }
    }
}
