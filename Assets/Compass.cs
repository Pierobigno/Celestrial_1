using UnityEngine;
using TMPro;

public class Compass : MonoBehaviour
{
    public Transform player;
    public Transform cameraPoint;
    public TextMeshProUGUI distanceText;

    void Update()
    {
        if(player != null)
        {
            Vector3 direction = player.position - cameraPoint.position;
            direction.Normalize();

            float distanceFromPlayer = Mathf.Round(Vector3.Distance(player.position, cameraPoint.position));
            distanceText.text = "Vous vous trouvez à " + distanceFromPlayer.ToString() + " mètres de distance";
        }
    }
}
