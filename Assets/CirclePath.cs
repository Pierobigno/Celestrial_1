using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePath : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject circleImpulsionParticlesPrefab;
    private GameObject circleImpulsionParticles;
    private Transform propelled;
    private PlayerMovement playerMovement;
    private bool isTriggered;
    public bool increaseSpeed;
    public bool destroyAllEnemies;
    public float circleExpansionSpeed;
    private bool increaseCircleRadius;
    public GameObject deathBycirclePathEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            if (!other.isTrigger && !isTriggered)
            {
                isTriggered = true;
                propelled = other.transform;
                playerMovement = propelled.root.GetComponent<PlayerMovement>();
                circleImpulsionParticles = Instantiate(circleImpulsionParticlesPrefab, propelled.position, propelled.rotation);
                StartCoroutine(TriggerCircle());
                if(increaseSpeed)
                {
                    StartCoroutine(IncreaseSpeedTemporarily());
                }
                if(destroyAllEnemies)
                {
                    List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

                    foreach (GameObject enemy in enemies)
                    {
                        enemy.GetComponent<Health>().Die();
                    }
                }
            }
        }
    }

    void Update()
    {
        if(propelled != null)
        {
            circleImpulsionParticles.transform.position = propelled.position;
        }

        if(increaseCircleRadius)
        {
            transform.GetChild(0).GetComponent<CircleAnimation>().radiusX += circleExpansionSpeed * Time.deltaTime;
            transform.GetChild(0).GetComponent<CircleAnimation>().radiusY += circleExpansionSpeed * Time.deltaTime;
        }
    }

    IEnumerator IncreaseSpeedTemporarily()
    {
        playerMovement.moveSpeed += 300;
        yield return new WaitForSeconds(1);
        playerMovement.moveSpeed -= 300;
    }

    IEnumerator TriggerCircle()
    {
        increaseCircleRadius = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
