using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // Ajoute cet objet à la liste des objets préservés
        DontDestroyOnLoadManager.AddObjectToDontDestroy(gameObject);
    }
}
