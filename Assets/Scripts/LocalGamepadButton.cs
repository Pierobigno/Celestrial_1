using UnityEngine;

public class LocalGamepadButton : MonoBehaviour
{
    public Sprite[] sprites;
    public string[] spriteNames;
    public string currentSpriteName;

    private void Start()
    {
        // S'assure que les tableaux ont la même taille
        if (sprites.Length != spriteNames.Length)
        {
            Debug.LogError("Les tableaux des sprites et des noms ne sont pas de la même taille !");
            return;
        }
    }

    private void Update()
    {
        // Vérifie si le nom du sprite est dans le tableau des noms
        int index = System.Array.IndexOf(spriteNames, currentSpriteName);

        if (index != -1)
        {
            // Change le sprite actuel en utilisant l'index trouvé
            GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
    }
}
