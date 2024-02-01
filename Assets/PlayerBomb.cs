using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBomb : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform bombStartPoint;
    public GameObject celesteCanon;

    void Start()
    {
        PlayerInput input = new PlayerInput();
        if(SceneManager.GetActiveScene().name != "SafePlace")
        {
            input.Player.Enable();
        }
        else
        {
            input.Player.Disable();
        } 
        input.Player.Bomb.canceled += x => Bomb_canceled();
    }

    void Bomb_canceled()
    {
        if(!celesteCanon.GetComponent<CelestePieceHealth>().isBroken)
        {
            PowerSlotsManager powerSlotsManager = FindObjectOfType<PowerSlotsManager>();
            if(powerSlotsManager.enablePowerSlots.Count > 0)
            {
                Instantiate(bombPrefab, bombStartPoint.position, bombStartPoint.rotation);
                powerSlotsManager.TriggerPowerSlots();
                Debug.Log("Bombe lancée");
            }
        }
    }
}
