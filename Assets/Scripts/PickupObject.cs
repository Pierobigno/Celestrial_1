using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupObject : MonoBehaviour
{
    public LayerMask playerLayer;
    public Transform player;
    private ItemsList itemsList;
    public int quantity;
    public bool isPickup = false;
    public float speed;
    private float distanceToPlayer;

    void Start()
    {
        player = FindObjectOfType<LucianMovement>().transform;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0 && !other.isTrigger && !isPickup)
        {
            player = other.transform.root;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= 0)
            {
                Pickup();
            }
        }
    }

    void Pickup()
    {
        // isPickup = true;
        // string name = GetComponent<ItemsList>().itemName;
        // Sprite itemSprite = GetComponent<ItemsList>().itemSprite;
        // string itemDescription = GetComponent<ItemsList>().itemDescription;
        // if(itemSprite != null && itemDescription != null)
        // {
        //     FindObjectOfType<Inventory>().AddItem(name, itemSprite, itemDescription);
        // }
        Debug.Log("PickUp() désactivé, revenir au script");
        Destroy(gameObject, 0.1f);
    }
}
