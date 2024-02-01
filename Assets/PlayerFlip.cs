using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject spaceship;
    public bool isFacingRight = true;

	public Vector2 orientation;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Gestion de l'orientation
		if(playerMovement.direction.x > 0){orientation.x = 1;}
		else if(playerMovement.direction.x < 0){orientation.x = -1;}

        if(orientation.x > 0 && !isFacingRight)
		{
			Flip();
		}
		else if(orientation.x < 0 && isFacingRight)
		{
			Flip();
		}
    }

    public void Flip()
	{	
		isFacingRight = !isFacingRight;
		spaceship.transform.Rotate(0f, 180f, 0f);
    }
}
