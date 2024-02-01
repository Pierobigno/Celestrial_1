using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameManager : MonoBehaviour
{
    public GameObject[] GameManagerObjectsToSpawnAtStart;

    void Awake()
    {
        CheckAndSpawnGameManagerObjects();
    }

    void CheckAndSpawnGameManagerObjects()
    {
        foreach (GameObject gmObject in GameManagerObjectsToSpawnAtStart)
        {
            // Vérifiez si un objet avec le même nom existe déjà dans la scène
            GameObject existingObject = GameObject.Find(gmObject.name);

            if (existingObject == null)
            {
                // Si aucun objet avec le même nom n'existe, instanciez l'objet
                Instantiate(gmObject, transform.position, transform.rotation);
            }
            // Sinon, ne faites rien car l'objet existe déjà
        }
    }
}
