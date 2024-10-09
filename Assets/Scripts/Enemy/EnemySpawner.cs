using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab do inimigo
    public int rows = 5;               // Número de fileiras
    public int columns = 11;           // Número de colunas
    public float spacing = 1.5f;       // Espaçamento entre os inimigos
    public Vector2 startPosition = new Vector2(-7, 3);  // Posição inicial para spawn

    private GameObject[,] enemies;     // Matriz para armazenar os inimigos

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        enemies = new GameObject[rows, columns];  // Inicializa a matriz de inimigos

        // Loop para instanciar inimigos em fileiras e colunas
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calcula a posição de cada inimigo baseado na posição inicial e no espaçamento
                Vector3 enemyPosition = new Vector3(startPosition.x + col * spacing, startPosition.y - row * spacing, 0);
                
                // Instancia o inimigo e o armazena na matriz
                GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
                enemies[row, col] = enemy;
            }
        }
    }
}
