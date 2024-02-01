using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LucianMovement : MonoBehaviour
{	
	[SerializeField] public float runSpeed = 20f;
	[SerializeField] public float airSpeed = 10f;	
	[SerializeField] public float speed;
	private Rigidbody2D rb;
	private PlayerInput input;
	private Gamepad gamepad;

	//Gestion du mouvement horizontal et vertical
	public bool isFacingRight = true;
	public float originalFallSpeed;
	public float fallSpeed;
	public bool moveOn;
	public Vector2 direction;
	public Vector2 orientation;
	public bool directionDriveByPlayer = true;

	public bool lookAtGroundFromGround;
	public bool lookAtSkyFromGround;


	[HideInInspector] public float gravityScaleOrigin;
	[HideInInspector] public float massOrigin;

	private Animator animator;

	//Gestion du regard
	private bool timerOn;
	private float timer;

	private void Awake()
	{
		input = new PlayerInput();
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		speed = runSpeed;
	}

	void Start()
	{
		gamepad = Gamepad.current;
		gravityScaleOrigin = rb.gravityScale;
		massOrigin = rb.mass;
		fallSpeed = originalFallSpeed;
	}	

	void FixedUpdate()
	{
		// Gestion de la gravité lors de la chute
        if (rb.velocity.y < 0) // Si le joueur tombe
        {
           rb.velocity = new Vector2(rb.velocity.x, fallSpeed);
        }
	}

	void Update()
	{	
		// Gestion de l'orientation
		if(direction.x != 0 && direction.x > 0.1f){orientation.x = 1;}
		if(direction.x != 0 && direction.x < 0){orientation.x = -1;}
		if(direction.y > 0){orientation.y = 1;}
		else if(direction.y < 0){orientation.y = -1;}
		else{orientation.y = 0;}


		animator.SetFloat("Speed", direction.x * orientation.x * speed);

		if(gamepad != null)
		{
			if(directionDriveByPlayer && !FindObjectOfType<Inventory>().isOpen)
			{
				direction = gamepad.leftStick.ReadValue();
			}
		}

		if(moveOn)
		{
			rb.velocity = new Vector2 (direction.x * speed, rb.velocity.y);
		}

		// Gestion du regard
		if(direction.y > 0 && GetComponent<LucianGroundDetection>().isGrounded)
		{
			timerOn = true;
			timer += Time.deltaTime;
			if(timer > 1f)
			{
				lookAtSkyFromGround = true;
				timerOn = false;
				timer = 0;
			}
		}
		
		else if(direction.y < 0 && GetComponent<LucianGroundDetection>().isGrounded)
		{
			timerOn = true;
			timer += Time.deltaTime;
			if(timer > 1f)
			{
				lookAtGroundFromGround = true;
				timerOn = false;
				timer = 0;
			}
		}
		else
		{
			lookAtSkyFromGround = false;
			lookAtGroundFromGround = false;
			timerOn = false;
			timer = 0;
		}
		
		if(direction.x != 0)
		{
			if((Mathf.Round(direction.y * 100f) * 0.01f) == 0)
			{
				moveOn = true;
			}
			else
			{
				moveOn = false;
			}
		}
		else
		{
			moveOn = false;
		}
		
		if (GetComponent<LucianGroundDetection>().isGrounded)
		{
			if(moveOn)
			{
				speed += Time.deltaTime;
			}

			if(!moveOn)
			{
				speed = runSpeed;
			}
		}

		if (!GetComponent<LucianGroundDetection>().isGrounded)
		{
			if(!moveOn)
			{
				speed = airSpeed;
			}

			if(moveOn && speed > runSpeed)
			{
				speed += Time.deltaTime;
			}
		}

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
		transform.Rotate(0f, 180f, 0f);
	}	

	public void GravityOn()
	{
		rb.bodyType = RigidbodyType2D.Dynamic;
	}

	public void GravityOff()
	{
		rb.bodyType = RigidbodyType2D.Static;
	}

	public void SwitchPlayerControls()
    {
        if(!directionDriveByPlayer)
        {
            directionDriveByPlayer = true;
        }
        else
        {
            directionDriveByPlayer = false;
        }

		direction = new Vector2(0,0);
        orientation = new Vector2(0,0);
    }
}