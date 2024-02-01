using UnityEngine;
using UnityEngine.AI; // Import du namespace
using Cinemachine;

public class OcclusionCulling : MonoBehaviour
{

  // Référence au GameObject sur lequel l'occlusion culling doit être activée
  public Camera mainCamera;
  public GameObject targetObject;
  public Renderer[] renderers;

  void Start()
  {
    mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    targetObject = gameObject;
  }

  void Update()
  {
    // Vérifiez si le GameObject est dans le champ de vision de la caméra
    if (IsInCameraView())
    {
      SetRenderers(true);
    }
   
    else
    {
      SetRenderers(false);
    }
  }

  public void SetRenderers(bool active)
  {
    foreach (Renderer renderer in renderers)
    {
      renderer.enabled = active;
    }
  }

  // Fonction qui vérifie si le GameObject est dans le champ de vision de la caméra
  bool IsInCameraView() // Appelée intrinsèquement dans Update()
  {
    // Créez un plan qui correspond au champ de vision de la caméra
    Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
    // Vérifiez si le GameObject est visible dans le champ de vision de la caméra
    renderers = targetObject.GetComponentsInChildren<Renderer>(true); //true pour gameObject + enfants (directs et ind) --- false pour enfants directs et ind --- () enfants directs
    Bounds bounds = new Bounds();
    foreach (Renderer renderer in renderers)
    {
      bounds.Encapsulate(renderer.bounds);
    }

    // Ajoutez une marge avant désactivation des renderers
    float margin = mainCamera.orthographicSize * mainCamera.aspect; // largeur de la caméra
    bounds.min = new Vector2(bounds.min.x - margin, bounds.min.y);
    bounds.max = new Vector2(bounds.max.x + margin, bounds.max.y);

    return GeometryUtility.TestPlanesAABB(planes, bounds);
  }

 void OnDrawGizmos()
{
  if(mainCamera != null)
  {
    // Dessinez les bords de la caméra en utilisant Gizmos
    Gizmos.color = Color.yellow;
    Vector3 cameraPos = mainCamera.transform.position;

    // Calculez la position des coins de la caméra dans l'espace monde
    Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, cameraPos.z));
    Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraPos.z));

  // Dessinez un cube (rectangle en 2D) qui correspond aux bords de la caméra
  Gizmos.DrawWireCube(new Vector3((topRight.x + bottomLeft.x) / 2, (topRight.y + bottomLeft.y) / 2, cameraPos.z), new Vector3(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y, 0));
  }
}
}
