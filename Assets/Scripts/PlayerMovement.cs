using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerTarget;
    public float flySpeed;
    public float moveSpeed;
    private CharacterStates characterStates;
    private PlayerJump playerJump;
    public Vector3 direction;
    private PlayerInput input;
	private Gamepad gamepad;
    public float turnInput;
    public float turnSpeed;

    public GameObject celesteCockpit;
    public GameObject celesteBase;
    public GameObject celesteWing;
    public GameObject celesteReactor;
    public GameObject celesteEngine;  

    void Awake()
    {
        gamepad = Gamepad.current;
    }  

    void Start()
    {
        input = new PlayerInput();
        characterStates = GetComponent<CharacterStates>();
        playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name != "SafePlace")
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
            
            if(!playerJump.jumpTargetOn)
            {
                if(gamepad != null)
                {
                    direction = gamepad.leftStick.ReadValue();
                }

                MovePlayerTarget(direction);
            }
        } 
    }

    void MovePlayerTarget(Vector2 direction)
    {
        direction.Normalize(); //Evite les deplacements diagonaux plus rapides
        playerTarget.Translate(direction * moveSpeed * Time.deltaTime);
    }
}

    // void Update()
    // {
    //     direction = gamepad.leftStick.ReadValue();

    //     MovePlayer(direction);
    // }

    // void MovePlayer(Vector2 direction)
    // {
    //     direction.Normalize(); //Evite les deplacements diagonaux plus rapides
    //     transform.Translate(direction * moveSpeed * Time.deltaTime);
    // }

