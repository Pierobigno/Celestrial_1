using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    [Header("Gestion de la vie")]
    public float maxHealth;
    public float currentHealth;

    [Header("Gestion de la mort")]
    public GameObject deathEffectPrefab;
    public float gamePoints;

    [Header("Gestion du drop")]
    public GameObject[] deathPickups;
    public Vector2 pickupOffset = new Vector2(0, 1);
    public Transform enemyDeathPoint;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(currentHealth <= damage)
        {
            Die();
        }
        else
        {
            currentHealth -= damage;
        }
        
    }    

    public void Die()
    {
        Instantiate(deathEffectPrefab, transform.position, transform.rotation);
        InstantiateDeathPickup();
        FindObjectOfType<EnemiesDeathCount>().currentEnemyDeathCount += 1;
        FindObjectOfType<GamePointsCalculator>().AddGamePoints(gamePoints);
        Destroy(gameObject);
    }

    void InstantiateDeathPickup() // A ajouter à la fin de l'animation de mort
	{
		if(deathPickups.Length > 0)
		{
			int randomIndex = Random.Range(0, deathPickups.Length);
			GameObject randomDeathPickup = deathPickups[randomIndex];
			Instantiate(randomDeathPickup, enemyDeathPoint.position + (Vector3)pickupOffset, Quaternion.identity);
			Destroy(gameObject);
		}
		else
		{
			Debug.LogWarning("Aucun objet deathPickup n'est assigné.");
			Destroy(gameObject);
		}
	}
}
