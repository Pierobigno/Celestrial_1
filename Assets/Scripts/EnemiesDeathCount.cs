using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDeathCount : MonoBehaviour
{
    public float currentEnemyDeathCount;

    void Start()
    {
        currentEnemyDeathCount = 0;
    }

	public void AddEnemyDeath(int enemyDeath)
	{
		currentEnemyDeathCount += enemyDeath;

	}
}
