using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionDuringFight : MonoBehaviour
{
    public float detectionRange = 10.0f;
    public LayerMask enemyMask;
    private EventStates eventStates;

    public bool enemyDetected;
    public Collider2D[] enemiesInRange;

    private FightEngaged fightEngaged;

    void Start()
    {
        fightEngaged = FindObjectOfType<FightEngaged>();
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
                EnemyDetectedDuringFight(); 
            }           
        }
        else
        {
            if(enemyDetected)
            {
                NoEnemyDetectedDuringFight();
            }
        }
    }

    private void EnemyDetectedDuringFight()
    {
        enemyDetected = true;
        Debug.Log("Le combat commence car il y a des ennemis (ne devrait pas s'afficher, si ce message s'affiche modifier le script)");
        if(enemyDetected)
        {
            return;
        }
    }

    private void NoEnemyDetectedDuringFight()
    {
        enemyDetected = false;
        fightEngaged.OnEngageFight(); // Les conditions dans fightEngaged font que le fight se désengage
        if(eventStates != null)
        {
            eventStates.isOver = true;
        }
        Debug.Log("Le combat s'arrête car il n'y a plus d'ennemi");
    }
}
