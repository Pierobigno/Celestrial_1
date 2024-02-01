using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public LayerMask playerLayer;
    private bool spawnTriggered;

    public bool oneByOne;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            if(!oneByOne)
            {
                if(!spawnTriggered)
                {
                    spawnTriggered = true;
                    GetComponent<SpawnObjects>().SpawnRandomObjects();
                }
            }
            else
            {
                if(!spawnTriggered)
                {
                    spawnTriggered = true;
                    GetComponent<SpawnObjects>().SpawnObjectsOneByOne();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            if(!oneByOne)
            {
                if(!spawnTriggered)
                {
                    spawnTriggered = true;
                    GetComponent<SpawnObjects>().SpawnRandomObjects();
                }
            }
            else
            {
                if(!spawnTriggered)
                {
                    spawnTriggered = true;
                    GetComponent<SpawnObjects>().SpawnObjectsOneByOne();
                }
            }
        }
    }
}
