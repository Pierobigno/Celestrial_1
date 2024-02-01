using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [HideInInspector] public Transform player;
    public bool followOnX;
    public bool followOnY;
    public bool followOnZ;
    public Vector3 offset;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 targetPosition = transform.position;

        if (followOnX)
        {
            targetPosition.x = player.position.x;
        }
        if (followOnY)
        {
            targetPosition.y = player.position.y;
        }
        if (followOnZ)
        {
            targetPosition.z = player.position.z;
        }

        transform.position = targetPosition + offset;
    }
}
