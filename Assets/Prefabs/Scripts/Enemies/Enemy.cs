using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject projectilePrefab;
    public AudioClip destructionSound; // Som para destruição
    public AudioClip shootingSound;    // Som para tiro
    private AudioSource _audioSource;  // Componente de áudio

    private float _timeSinceLastShot;
    private bool _movingRight = true; // Controla a direção de movimento
    private float _dropAmount = 0.5f; // Distância que o inimigo desce ao mudar de direção
    private float _shootingInterval;  // Agora o intervalo será aleatório
    
    // Adicionando a variável para a quantidade de tiros
    private int requiredHits;  // Tiros necessários para derrotar o boss
    private int currentHits;    // Contador de tiros recebidos

    void Start()
    {
        // Define um intervalo de tiro aleatório entre 1 e 3 segundos para cada inimigo
        _shootingInterval = Random.Range(1f, 3f);
        _audioSource = GetComponent<AudioSource>(); // Obtém o componente de som
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        // Move horizontalmente
        float horizontalMovement = _movingRight ? speed * Time.deltaTime : -speed * Time.deltaTime;
        transform.Translate(Vector2.right * horizontalMovement);

        // Limite horizontal da tela (meio da câmera)
        float screenLimitX = Camera.main.orthographicSize * Camera.main.aspect;

        if (transform.position.x > screenLimitX && horizontalMovement > 0)
        {
            // Inverte a direção
            _movingRight = !_movingRight;

            // Move o inimigo para baixo somente uma vez após atingir a borda
            float newY = transform.position.y - _dropAmount;
            transform.position = new Vector2(transform.position.x, newY);

            // Verifica se o inimigo está saindo da tela para destruí-lo
            if (transform.position.y < -Camera.main.orthographicSize)
            {
                Destroy(gameObject); // Destrói o inimigo se sair da tela
            }
        }
        if (transform.position.x < -screenLimitX && horizontalMovement < 0)
        {
            // Inverte a direção
            _movingRight = !_movingRight;

            // Move o inimigo para baixo somente uma vez após atingir a borda
            float newY = transform.position.y - _dropAmount;
            transform.position = new Vector2(transform.position.x, newY);

            // Verifica se o inimigo está saindo da tela para destruí-lo
            if (transform.position.y < -Camera.main.orthographicSize)
            {
                Destroy(gameObject); // Destrói o inimigo se sair da tela
            }
        }
    }


    void Shoot()
    {
        _timeSinceLastShot += Time.deltaTime;

        if (_timeSinceLastShot >= _shootingInterval)
        {
            if (CompareTag("Boss"))
            {
                _audioSource.PlayOneShot(shootingSound);
                Instantiate(projectilePrefab, transform.position, Quaternion.identity); // Disparo central
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -30)); // Disparo à direita
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 30)); // Disparo à esquerda
                _timeSinceLastShot = 0;
                // Atualiza o intervalo de tiro para ser aleatório novamente
                _shootingInterval = Random.Range(1f, 2f);
            }
            else
            {
                _audioSource.PlayOneShot(shootingSound);
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                _timeSinceLastShot = 0;
                // Atualiza o intervalo de tiro para ser aleatório novamente
                _shootingInterval = Random.Range(2f, 6f);
            }
        }
    }

    public event System.Action OnEnemyDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (CompareTag("Boss"))
        {
            if (collision.gameObject.CompareTag("PlayerProjectile"))
            {
                currentHits++; // Incrementa o contador de tiros recebidos

                // Verifica se o boss recebeu tiros suficientes
                if (currentHits >= requiredHits)
                {
                    _audioSource.PlayOneShot(destructionSound); // Toca som de destruição
                    Destroy(gameObject); // Destrói o boss se ele foi derrotado
                }

                Destroy(collision.gameObject); // Destrói o projétil
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("PlayerProjectile"))
            {
                _audioSource.PlayOneShot(destructionSound); // Toca som de destruição
                Destroy(gameObject); // Inimigo destruído quando atingido
                Destroy(collision.gameObject); // Destrói o projétil também
                OnEnemyDestroyed?.Invoke(); // Notifica o spawner que o inimigo foi destruído
            }
        }
    }
    public void InitializeMiniboss()
    {
        _movingRight = !_movingRight; // Inicia na direção oposta
    }

    public void InitializeBoss(int waveCount)
    {
        _movingRight = !_movingRight; // Inicia na direção oposta
        requiredHits = waveCount; // Define o número de tiros necessários para derrotar o boss
        currentHits = 0; // Reinicia o contador de tiros recebidos
    }

}