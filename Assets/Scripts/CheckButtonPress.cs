using UnityEngine;
using UnityEngine.InputSystem;

public class CheckButtonPress : MonoBehaviour
{
    private PlayerInput input;
    private float timer;

    public int inputID;
    public float inputResetCooldown = 0.02f;

    void Start()
    {     
        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Action.canceled += x => Action_canceled();
        input.Player.DashAttack.canceled += x => Bomb_canceled();
    }

    void Update()
    {
       timer += Time.deltaTime;
       if(timer > inputResetCooldown)
        {
            timer = 0;
            ResetInput();
        }
    }

    void ResetInput()
    {
        inputID = 0;
    }

    void Action_canceled()
    {
        inputID = 1;
    }

    void Bomb_canceled()
    {
        inputID = 2;
    }
}