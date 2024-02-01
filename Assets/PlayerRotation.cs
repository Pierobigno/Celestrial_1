using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float returnSpeed = 10f;
    private Vector2 direction;
    private Quaternion initialRotation;
    public Transform spaceship;

    private PlayerInput input;
    private Gamepad gamepad;

    public bool resetPosition;
    public bool rotationTriggerOn;

    void Awake()
    {
        gamepad = Gamepad.current;
    }

    void Start()
    {
        input = new PlayerInput();
        input.Player.Enable();
        initialRotation = spaceship.rotation;
    }

    void Update()
    {
        if(gamepad != null && SceneManager.GetActiveScene().name != "SafePlace")
        {
            direction = gamepad.rightStick.ReadValue();
        }

        if(rotationTriggerOn)
        {
            if(Mathf.Round(direction.y) == -1)
            {
                Down();
            }
            else if (Mathf.Round(direction.y) == 1)
            {
                Up();
            }
            else
            {
                if(resetPosition)
                {
                    spaceship.rotation = Quaternion.Lerp(spaceship.rotation, initialRotation, returnSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            if(resetPosition)
            {
                spaceship.rotation = Quaternion.Lerp(spaceship.rotation, initialRotation, returnSpeed * Time.deltaTime);
            }
        }
    }

    void OnRotationTrigger()
    {
        rotationTriggerOn = !rotationTriggerOn;
    }

    void Up()
    {
        spaceship.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void Down()
    {
        spaceship.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
    }
}
