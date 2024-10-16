using UnityEngine;
using TMPro;  // Certifique-se de adicionar esta linha para usar TextMeshPro

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject minibossPrefab;
    public GameObject bossPrefab;
    public int initialRows = 3;
    public int initialColumns = 5;
    public float initialSpacing = 1.5f;
    public float spawnHeightOffset = 1f;
    public float spawnWidthOffset = -5f;
    public float initialEnemySpeed; // Velocidade inicial dos inimigos
    public float speedIncrement = 1.05f;  // Aumento de velocidade por onda

    public TextMeshProUGUI waveCounterText;  // Referência ao texto da UI TextMeshPro

    private int _currentRows;
    private int _currentColumns;
    private float _currentSpacing;
    private float _currentEnemySpeed;
    private int _waveCount = 1;  // Contador de ondas

    void Start()
    {
        _currentEnemySpeed = initialEnemySpeed;
        SpawnEnemies(initialRows, initialColumns, initialSpacing);
        UpdateWaveCounter();  // Atualiza o texto na tela ao iniciar o jogo
    }

    void Update()
    {
        // Verifica se todos os inimigos foram destruídos antes de spawnar nova onda
        if (FindObjectsOfType<Enemy>().Length == 0)
        {
            _waveCount++; // Incrementa o contador de ondas
            if (_waveCount % 10 == 0)
            {
                SpawnBoss();
                UpdateWaveCounter();
                spawnHeightOffset = 3f;
            }
            // Cria Miniboss a cada 5 ondas
            else if (_waveCount % 5 == 0)
            {
                SpawnMiniboss();
                UpdateWaveCounter();  // Atualiza o texto na tela
                spawnHeightOffset = 5f;
            }
            else
            {
                int newRows = Random.Range(1, 4);
                if (newRows < 3)
                {
                    spawnHeightOffset = 3f;
                }
                else
                {
                    spawnHeightOffset = 1f;
                }
                int newColumns = Random.Range(3, 7); // Nova quantidade aleatória de colunas

                int newSpacing = Random.Range(1, 3);

                _currentEnemySpeed *= speedIncrement; // Aumenta a velocidade dos inimigos
                SpawnEnemies(newRows, newColumns, newSpacing);
                UpdateWaveCounter();  // Atualiza o texto na tela
            }
        }
    }

        void SpawnEnemies(int rows, int columns, float spacing)
        {
            _currentRows = rows;
            _currentColumns = columns;
            _currentSpacing = spacing;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Vector2 spawnPosition = new Vector2(col * spacing + spawnWidthOffset, row * spacing + spawnHeightOffset);
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);

                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    enemyScript.speed = _currentEnemySpeed;
                }
            }
        }
        void SpawnMiniboss()
        {
            Vector2 spawnPosition = new Vector2(spawnWidthOffset, spawnHeightOffset = 4f);
            GameObject miniboss = Instantiate(minibossPrefab, spawnPosition, Quaternion.identity, transform);
            Enemy minibossScript = miniboss.GetComponent<Enemy>();

            // Definindo a velocidade do miniboss
            minibossScript.speed = _waveCount;

            // Inicializa o Miniboss
            minibossScript.InitializeMiniboss();
        }

    void SpawnBoss()
    {
        Vector2 spawnPosition = new Vector2(spawnWidthOffset, spawnHeightOffset = 3f);
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity, transform);
        Enemy bossScript = boss.GetComponent<Enemy>();

        bossScript.speed = _waveCount * 0.4f; // Definindo a velocidade do boss
        bossScript.InitializeBoss(_waveCount); // Passando o número da onda para o boss
    }

    // Atualiza o texto UI com a contagem de ondas
    void UpdateWaveCounter()
        {
            if (waveCounterText != null)
            {
                waveCounterText.text = "Wave: " + _waveCount;
            }
        }
}