using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange;
    public LayerMask enemyMask;
    private EventStates eventStates;

    public bool enemyDetected;
    public Collider2D[] enemiesInRange;

    void Start()
    {
        eventStates = GetComponent<EventStates>();
    }

    private void Update()
    {
        // Récupére tous les colliders dans la zone de détection
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyMask);

        // Crée un tableau temporaire pour stocker les ennemis sans trigger
        Collider2D[] tempEnemies = new Collider2D[colliders.Length];
        int tempIndex = 0;

        // Filtre les colliders avec un isTrigger = true
        foreach (Collider2D collider in colliders)
        {
            if (!collider.isTrigger)
            {
                tempEnemies[tempIndex] = collider;
                tempIndex++;
            }
        }

        // Mettre à jour le tableau enemiesInRange avec les ennemis détectés sans trigger
        enemiesInRange = new Collider2D[tempIndex];
        for (int i = 0; i < tempIndex; i++)
        {
            enemiesInRange[i] = tempEnemies[i];
        }

        if(enemiesInRange.Length > 0)
        {
            if(!enemyDetected)
            {
                enemyDetected = true;
            }           
        }
        else
        {
            if(enemyDetected)
            {
                enemyDetected = false;
            }
        }
    }
}
