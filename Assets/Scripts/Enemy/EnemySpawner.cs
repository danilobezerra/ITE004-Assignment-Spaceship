using UnityEngine;
using System.Collections.Generic;

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

    void AdjustSpawnPosition()
    {
        // Ajusta a posição de início para centralizar os inimigos na tela
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
                // Calcula a posição de cada inimigo baseado na posição inicial e no espaçamento
                Vector3 enemyPosition = new Vector3(startPosition.x + col * spacing, startPosition.y - row * spacing, 0);

                // Instancia o inimigo e o armazena na matriz
                GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
                enemies[row, col] = enemy;

                // Adiciona os inimigos da fileira mais baixa na lista de disparo
                if (row == rows - 1)
                {
                    bottomRowEnemies.Add(enemy);
                }
            }
        }
    }
    void RandomEnemyShoot()
    {
        if (bottomRowEnemies.Count > 0)
        {
            // Seleciona um inimigo aleatório da fileira mais baixa para disparar
            int randomIndex = Random.Range(0, bottomRowEnemies.Count);
            GameObject randomEnemy = bottomRowEnemies[randomIndex];

            if (randomEnemy != null)
            {
                randomEnemy.GetComponent<Enemy>().Bullet();
            }
        }
    }
}
