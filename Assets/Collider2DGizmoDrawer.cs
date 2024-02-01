using UnityEngine;

public class Collider2DGizmoDrawer : MonoBehaviour
{
    public Color color;
    private Collider2D collider;
    private void OnDrawGizmos()
    {
        collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            Gizmos.color = color;

            if (collider is BoxCollider2D)
            {
                DrawBoxGizmo((BoxCollider2D)collider);
            }
            else if (collider is CircleCollider2D)
            {
                DrawCircleGizmo((CircleCollider2D)collider);
            }
            else if (collider is CapsuleCollider2D)
            {
                DrawCapsuleGizmo((CapsuleCollider2D)collider);
            }
        }
    }

    private void DrawBoxGizmo(BoxCollider2D boxCollider)
    {
        Vector2 center = boxCollider.offset;
        Vector2 size = boxCollider.size;

        // Transformation des points locaux en coordonnées mondiales
        Vector2 min = (Vector2)transform.TransformPoint(center - size * 0.5f);
        Vector2 max = (Vector2)transform.TransformPoint(center + size * 0.5f);

        Gizmos.DrawWireCube((min + max) * 0.5f, max - min);
    }

    private void DrawCircleGizmo(CircleCollider2D circleCollider)
    {
        Vector2 center = circleCollider.offset;
        float radius = circleCollider.radius;

        // Transformation du point local en coordonnées mondiales
        Vector2 worldCenter = (Vector2)transform.TransformPoint(center);

        Gizmos.DrawWireSphere(worldCenter, radius);
    }

    private void DrawCapsuleGizmo(CapsuleCollider2D capsuleCollider)
    {
        Vector2 center = capsuleCollider.offset;
        float sizeX = capsuleCollider.size.x;
        float sizeY = capsuleCollider.size.y;
        float radius = Mathf.Min(sizeX, sizeY) * 0.5f;

        // Transformation du point local en coordonnées mondiales
        Vector2 worldCenter = (Vector2)transform.TransformPoint(center);

        // Dessiner les extrémités de la capsule avec des sphères
        Gizmos.DrawWireSphere(worldCenter + new Vector2(0, sizeY * 0.5f - radius), radius);
        Gizmos.DrawWireSphere(worldCenter - new Vector2(0, sizeY * 0.5f - radius), radius);

        // Dessiner le corps de la capsule avec un cube
        Gizmos.DrawWireCube(worldCenter, new Vector3(sizeX, sizeY - radius * 2, 0));
    }
}
