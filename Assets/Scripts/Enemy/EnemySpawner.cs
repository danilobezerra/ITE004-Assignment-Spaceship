using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int rows = 5;
    public int columns = 11;
    public float spacing = 1.5f;
    public Vector2 startPosition = new Vector2(-7, 3);
    public float screenMargin = 1f;
    public float shootInterval = 4f;

    private GameObject[,] enemies;
    private List<GameObject> bottomRowEnemies = new List<GameObject>();

    void Start()
    {
        AdjustSpawnPosition();
        SpawnEnemies();
        InvokeRepeating("RandomEnemyShoot", shootInterval, shootInterval);
    }

    void Update()
    {
        // Verifica se todos os inimigos foram eliminados
        if (AreAllEnemiesDead())
        {
            RestartGame();
        }
    }

    void AdjustSpawnPosition()
    {
        float screenWidth = Camera.main.aspect * Camera.main.orthographicSize * 2;
        float totalEnemyWidth = (columns - 1) * spacing;
        startPosition.x = -(totalEnemyWidth / 2) + screenMargin;
    }

    void SpawnEnemies()
    {
        enemies = new GameObject[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 enemyPosition = new Vector3(startPosition.x + col * spacing, startPosition.y - row * spacing, 0);
                GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
                enemies[row, col] = enemy;

                if (row == rows - 1)
                {
                    bottomRowEnemies.Add(enemy);
                }
            }
        }
    }

    public bool AreAllEnemiesDead()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false; // Se um inimigo ainda existir, retorna falso
            }
        }
        return true; // Todos os inimigos foram destruídos
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RandomEnemyShoot()
    {
        if (bottomRowEnemies.Count > 0)
        {
            int numberOfShooters = Random.Range(1, bottomRowEnemies.Count + 1);
            List<int> indicesToShoot = new List<int>();

            while (indicesToShoot.Count < numberOfShooters)
            {
                int randomIndex = Random.Range(0, bottomRowEnemies.Count);
                if (!indicesToShoot.Contains(randomIndex))
                {
                    indicesToShoot.Add(randomIndex);
                }
            }

            foreach (int index in indicesToShoot)
            {
                GameObject shooter = bottomRowEnemies[index];
                if (shooter != null)
                {
                    shooter.GetComponent<Enemy>().Bullet();
                }
            }
        }
    }
}
