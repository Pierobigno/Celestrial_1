using UnityEngine;

public class AxisMundi : MonoBehaviour
{
    public Transform player;
    public Transform cameraPoint;
    // private float springForce = 100.0f;
    // private float damper = 10.0f;
    // public float ropeWidth = 0.1f;
    public float distance;
    private Color color;
    private float alpha;

    private LineRenderer lineRenderer;
    private SpringJoint springJoint;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if (springJoint == null)
        {
            springJoint = gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = Vector3.zero;
            // springJoint.spring = springForce;
            // springJoint.damper = damper;
        }

        // lineRenderer.startWidth = ropeWidth;
        // lineRenderer.endWidth = ropeWidth;

        // calculate the distance between
        distance = Vector3.Distance(player.position, cameraPoint.position);
        color = new Color(255, 255, 255, alpha);
        alpha = distance * 10;

        // update the rope position
        Vector3 midpoint = (player.position + cameraPoint.position) / 2.0f;
        transform.position = midpoint;

        // update the line renderer positions
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, cameraPoint.position);
    }
}
