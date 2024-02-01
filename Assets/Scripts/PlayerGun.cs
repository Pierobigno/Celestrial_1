using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGun : MonoBehaviour
{
    public GameObject playerShotPrefab;
    public GameObject playerCanonSmokePrefab;
    public Transform playerShotPoint;
    private PlayerInput input;
    private bool timerOn;
    private float timer;
    public float shootCooldown;
    public bool isShooting;
    private bool canShoot;

    // GameObjets Instantiés
    public List<GameObject> playerCanonSmokes = new List<GameObject>();
    private GameObject playerShot;
    private GameObject playerCanonSmoke;

    void Start()
    {
        input = new PlayerInput();
        if(SceneManager.GetActiveScene().name != "SafePlace")
        {
            input.Player.Enable();
        }
        else
        {
            input.Player.Disable();
        }        
        input.Player.Shoot.performed += x => Shoot_performed();
        input.Player.Shoot.canceled += x => Shoot_canceled();
        canShoot = true;
    }

    void Update()
    {
        playerCanonSmokes.RemoveAll(item => item == null);

        if(playerCanonSmokes.Count > 0)
        {
            foreach (GameObject playerCanonSmoke in playerCanonSmokes)
            {
                playerCanonSmoke.transform.position = playerShotPoint.position;
            }
        }

        if(isShooting && canShoot)
        {
            timerOn = true;
            playerShot = Instantiate(playerShotPrefab, playerShotPoint.position, playerShotPoint.rotation);
            playerCanonSmoke = Instantiate(playerCanonSmokePrefab, playerShotPoint.position, playerShotPoint.rotation);
            playerCanonSmokes.Add(playerCanonSmoke);
        }

        if(timerOn)
        {
            timer += Time.deltaTime;
            if(timer > shootCooldown)
            {
                timerOn = false;
                timer = 0f;
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }
        }
    }

    void Shoot_performed()
    {
        isShooting = true;
    }

    void Shoot_canceled()
    {
        isShooting = false;
    }
}
