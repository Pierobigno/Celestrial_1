using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float maxDashChargeTime = 1.0f;
    public float dashDuration = 0.2f;
    public GameObject chargeDashParticles;
    public GameObject explosionDash;
    public GameObject dashParticles;
    public Transform dashParticlesPoint;

    private Rigidbody2D rb;
    public float dashChargeTime = 0f;
    private PlayerInput input;
    private float chargeDashTime;
    private bool chargeDashTimerOn;
    private float chargeDashTimer;
    private bool isChargingDash;
    private PlayerMovement playerMovement;
    private PowerSlotsManager powerSlotsManager;
    public bool isDashing;

    private void Start()
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
        input.Player.DashAttack.performed += x => DashAttack_performed();
        input.Player.DashAttack.canceled += x => DashAttack_canceled();
        playerMovement = GetComponent<PlayerMovement>();
        powerSlotsManager = FindObjectOfType<PowerSlotsManager>();
    }

    void DashAttack_performed()
    {
        if(powerSlotsManager.enablePowerSlots.Count > 0)
        {
            ChargeDash();
        }
    }

    void DashAttack_canceled()
    {
        if(isChargingDash && !isDashing)
        {
            StartDash();
            Debug.Log("Lance le dash");
        }
        
    }

    void ChargeDash()
    {
        isChargingDash = true;
        dashChargeTime = 0f;
        chargeDashTimerOn = true;
        Instantiate(chargeDashParticles, transform.position, transform.rotation);
    }

    void Update()
    {
        if(chargeDashTimerOn)
        {
            dashChargeTime += Time.deltaTime;
            if(dashChargeTime > maxDashChargeTime)
            {
                StartDash();
            }
        }

        if(isDashing)
        {
            transform.position += transform.right * dashSpeed * dashChargeTime;
        }
    }

    private void StartDash()
    {
        isChargingDash = false; //Empêche le declenchement du canceled si isChargingDash toujours appuyé

        if(powerSlotsManager.enablePowerSlots.Count > 0)
        {
            powerSlotsManager.TriggerPowerSlots();
            isDashing = true;
            chargeDashTimerOn = false;
            Instantiate(explosionDash, transform.position, transform.rotation);
            InstantiateDashParticles();
            StartCoroutine(EndDash());         
        }
    }

    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashDuration);
        StopDash();
    }

    private void StopDash()
    {
        isDashing = false;
        DisableDashParticles();
    }
    
    public void InstantiateDashParticles()
    {
        GameObject DashTrail = Instantiate(dashParticles, dashParticlesPoint.position, dashParticlesPoint.rotation);
        DashTrail.transform.parent = transform;

        GameObject[] dashParticlesToDestroyPrefabs = GameObject.FindGameObjectsWithTag("DashParticles");
        foreach (GameObject dashParticlesToDestroyPrefab in dashParticlesToDestroyPrefabs)
        {
            ParticleSystem dashParticles = dashParticlesToDestroyPrefab.GetComponentInChildren<ParticleSystem>();
            var emission = dashParticles.emission;
            emission.rateOverDistance = 30;
        }
    }

    public void DisableDashParticles()
    {
        GameObject[] dashParticlesToDestroyPrefabs = GameObject.FindGameObjectsWithTag("DashParticles");
        foreach (GameObject dashParticlesToDestroyPrefab in dashParticlesToDestroyPrefabs)
        {
            ParticleSystem dashParticles = dashParticlesToDestroyPrefab.GetComponentInChildren<ParticleSystem>();
            var emission = dashParticles.emission;
            emission.rateOverDistance = 0;
        }
    }
}
