using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadManager : MonoBehaviour
{
    private static DontDestroyOnLoadManager instance;
    private static List<GameObject> preservedObjects = new List<GameObject>();

    private void Awake()
    {
        // Assure qu'il n'y a qu'une seule instance de ce gestionnaire
        if (instance == null)
        {
            // Définit cette instance comme la seule instance
            instance = this;

            // Empêche la destruction de l'objet lors du changement de scène
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Détruit l'ancienne instance et remplace par la nouvelle
            Destroy(instance.gameObject);
            instance = this;

            // Empêche la destruction de l'objet lors du changement de scène
            DontDestroyOnLoad(gameObject);
        }
    }

    // Ajoutez ici des méthodes pour ajouter des objets à préserver
    public static void AddObjectToDontDestroy(GameObject obj)
    {
        // Vérifie si l'objet n'a pas déjà été préservé
        if (!preservedObjects.Contains(obj))
        {
            // Ajoute l'objet à la liste des objets préservés
            preservedObjects.Add(obj);

            // Empêche la destruction de l'objet lors du changement de scène
            DontDestroyOnLoad(obj);
        }
    }
}
