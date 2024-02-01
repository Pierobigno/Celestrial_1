using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpTarget : MonoBehaviour
{
    private PlayerInput input;
	private Gamepad gamepad;
    private PlayerJump playerJump;

    public Vector3 direction;
    public float moveSpeed;

    void Start()
    {
        input = new PlayerInput();
        gamepad = Gamepad.current;
        playerJump = FindObjectOfType<PlayerJump>();
    }

void Update()
    {
        if(playerJump.jumpTargetOn)
        {
            direction = gamepad.leftStick.ReadValue();
            MoveJumpTarget(direction);
        }        
    }

    void MoveJumpTarget(Vector2 direction)
    {
        direction.Normalize(); //Evite les deplacements diagonaux plus rapides
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

}
