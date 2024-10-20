using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject projectilePrefab;
    public AudioClip destructionSound; // Som para destrui��o
    public AudioClip shootingSound;    // Som para tiro
    private AudioSource _audioSource;  // Componente de �udio

    private float _timeSinceLastShot;
    private bool _movingRight = true; // Controla a dire��o de movimento
    private float _dropAmount = 0.5f; // Dist�ncia que o inimigo desce ao mudar de dire��o
    private float _shootingInterval;  // Agora o intervalo ser� aleat�rio
    
    // Adicionando a vari�vel para a quantidade de tiros
    private int requiredHits;  // Tiros necess�rios para derrotar o boss
    private int currentHits;    // Contador de tiros recebidos

    void Start()
    {
        // Define um intervalo de tiro aleat�rio entre 1 e 3 segundos para cada inimigo
        _shootingInterval = Random.Range(1f, 3f);
        _audioSource = GetComponent<AudioSource>(); // Obt�m o componente de som
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

        // Limite horizontal da tela (meio da c�mera)
        float screenLimitX = Camera.main.orthographicSize * Camera.main.aspect;

        if (transform.position.x > screenLimitX && horizontalMovement > 0)
        {
            // Inverte a dire��o
            _movingRight = !_movingRight;

            // Move o inimigo para baixo somente uma vez ap�s atingir a borda
            float newY = transform.position.y - _dropAmount;
            transform.position = new Vector2(transform.position.x, newY);

            // Verifica se o inimigo est� saindo da tela para destru�-lo
            if (transform.position.y < -Camera.main.orthographicSize)
            {
                Destroy(gameObject); // Destr�i o inimigo se sair da tela
            }
        }
        if (transform.position.x < -screenLimitX && horizontalMovement < 0)
        {
            // Inverte a dire��o
            _movingRight = !_movingRight;

            // Move o inimigo para baixo somente uma vez ap�s atingir a borda
            float newY = transform.position.y - _dropAmount;
            transform.position = new Vector2(transform.position.x, newY);

            // Verifica se o inimigo est� saindo da tela para destru�-lo
            if (transform.position.y < -Camera.main.orthographicSize)
            {
                Destroy(gameObject); // Destr�i o inimigo se sair da tela
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
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -30)); // Disparo � direita
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 30)); // Disparo � esquerda
                _timeSinceLastShot = 0;
                // Atualiza o intervalo de tiro para ser aleat�rio novamente
                _shootingInterval = Random.Range(1f, 2f);
            }
            else
            {
                _audioSource.PlayOneShot(shootingSound);
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                _timeSinceLastShot = 0;
                // Atualiza o intervalo de tiro para ser aleat�rio novamente
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
                    _audioSource.PlayOneShot(destructionSound); // Toca som de destrui��o
                    Destroy(gameObject); // Destr�i o boss se ele foi derrotado
                }

                Destroy(collision.gameObject); // Destr�i o proj�til
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("PlayerProjectile"))
            {
                _audioSource.PlayOneShot(destructionSound); // Toca som de destrui��o
                Destroy(gameObject); // Inimigo destru�do quando atingido
                Destroy(collision.gameObject); // Destr�i o proj�til tamb�m
                OnEnemyDestroyed?.Invoke(); // Notifica o spawner que o inimigo foi destru�do
            }
        }
    }
    public void InitializeMiniboss()
    {
        _movingRight = !_movingRight; // Inicia na dire��o oposta
    }

    public void InitializeBoss(int waveCount)
    {
        _movingRight = !_movingRight; // Inicia na dire��o oposta
        requiredHits = waveCount; // Define o n�mero de tiros necess�rios para derrotar o boss
        currentHits = 0; // Reinicia o contador de tiros recebidos
    }

}