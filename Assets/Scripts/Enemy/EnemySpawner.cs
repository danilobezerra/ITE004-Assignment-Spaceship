using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public float spawnInterval = 2f; // Intervalo entre spawns de grupos de inimigos
    public int initialSpawnCount = 9; // Número inicial de inimigos a serem spawnados
    public int groupSize = 4; // Número de inimigos por grupo

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
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 5f, 0f); // Y=5 para spawn no topo
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(newEnemy);

        // Ajusta a velocidade do inimigo
        float randomFallSpeed = Random.Range(1f, 5f); // Velocidade aleatória entre 1 e 5
        newEnemy.GetComponent<EnemyMovement>().fallSpeed = randomFallSpeed; // Ajusta a velocidade do inimigo
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
