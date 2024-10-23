using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float spawnInterval = 2f; 
    public int initialSpawnCount = 9; 
    public int groupSize = 4;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        // Spawn inicial de inimigos
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnEnemy();
        }

        StartCoroutine(SpawnGroupsOfEnemies());
    }

    private IEnumerator SpawnGroupsOfEnemies()
    {
        while (true)
        {
            for (int i = 0; i < groupSize; i++)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 5f, 0f);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(newEnemy);

        float randomFallSpeed = Random.Range(1f, 5f);
        newEnemy.GetComponent<EnemyMovement>().fallSpeed = randomFallSpeed;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
