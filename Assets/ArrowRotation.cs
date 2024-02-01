using UnityEngine;
using UnityEngine.UI;

public class ArrowRotation : MonoBehaviour
{
    public Transform player; // Assurez-vous de définir votre joueur dans l'inspecteur Unity
    public RectTransform arrowTransform; // Assurez-vous de définir la flèche dans l'inspecteur Unity
    public float radius = 50f; // Rayon autour du point central

    void Update()
    {
        if (player != null)
        {
            // Convertissez la position du joueur du monde à la position de l'UI
            Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

            // Obtenez la direction du joueur par rapport au point central
            Vector3 directionToPlayer = playerScreenPos - (Vector2)arrowTransform.position;

            // Calculez l'angle en radians et convertissez-le en degrés
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Calculez la nouvelle position en utilisant l'angle et le rayon
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            // Définissez la nouvelle position de la flèche
            arrowTransform.anchoredPosition = new Vector2(x, y);

            // Faites pivoter la flèche pour qu'elle pointe toujours vers le joueur
            arrowTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
