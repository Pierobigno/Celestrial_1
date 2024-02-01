using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int numberOfObjectsToSpawn = 10;
    public Vector3 spawnAreaSize;

    public float timeBetweenSpawns = 1.0f;
    public bool unlimited;

    public void SpawnRandomObjects()
    {
        int objectsToSpawn = unlimited ? int.MaxValue : numberOfObjectsToSpawn;
        for (int i = 0; i < objectsToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
    }

    public void SpawnObjectsOneByOne()
    {
        StartCoroutine(SpawnObjectsOneByOneCoroutine());
    }

    IEnumerator SpawnObjectsOneByOneCoroutine()
    {
        int objectsToSpawn = unlimited ? int.MaxValue : numberOfObjectsToSpawn;
        for (int i = 0; i < objectsToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);

        return new Vector3(randomX, randomY, 0f) + transform.position;
    }
}
