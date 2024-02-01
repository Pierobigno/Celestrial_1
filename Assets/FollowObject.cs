using UnityEngine;

public class FollowPlayerTarget : MonoBehaviour
{
    public Transform objectToFollow;
    public string nameOfTheObjectToFollow;
    public bool followOnX;
    public bool followOnY;
    public bool followOnZ;
    public float speed;

    void Start()
    {
        if(objectToFollow == null)
        {
            objectToFollow = GameObject.Find(nameOfTheObjectToFollow).transform;
        }
    }

    void Update()
    {
        if(followOnX)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, objectToFollow.position.x, speed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );
        }
        if(followOnY)
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, objectToFollow.position.y, speed * Time.deltaTime),
                transform.position.z
            );
        }
        if(followOnZ)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                Mathf.MoveTowards(transform.position.z, objectToFollow.position.z, speed * Time.deltaTime)
            );
        }
    }
}
