using UnityEngine;
using TMPro;  // Importa a biblioteca do TextMeshPro

public class SpaceshipEngine : MonoBehaviour, IMovementController, IGunController
{
    public Projectile projectilePrefab;      // Projétil normal
    public Projectile bigProjectilePrefab; // Projétil maior
    public Spaceship spaceship;
    [SerializeField] float fireRate = 0.5f;  // Taxa de disparo
    private float nextFire;  // Controla o tempo para o próximo disparo
    public AudioClip laserLarge_002;  // Som de projétil grande
    private AudioSource audioSource;  // Referência ao componente AudioSource

    public float bigProjectileCooldown = 5.0f;  // Tempo de recarga para o disparo do projétil grande
    private float lastBigProjectileTime;  // Controla o tempo do último disparo grande

    // Referência ao TextMeshProUGUI que vai mostrar o status do BigProjectile
    public TextMeshProUGUI bigProjectileStatusText;

    private void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);

        // Verifica se há um AudioSource, senão adiciona um
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Inicializa o texto do status como "Carregando..."
        if (bigProjectileStatusText != null)
        {
            bigProjectileStatusText.text = "Carregando especial...";
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
        UpdateBigProjectileStatus();  // Atualiza o texto do status do BigProjectile
    }

    // Movimentação da nave
    private void HandleMovement()
    {
        // Movimentação horizontal e vertical com as teclas configuradas
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0)  // Evita movimentação nula
        {
            spaceship.MoveHorizontally(horizontalInput);
        }

        if (Mathf.Abs(verticalInput) > 0)  // Evita movimentação nula
        {
            spaceship.MoveVertically(verticalInput);
        }
    }

    // Controle de disparos
    private void HandleShooting()
    {
        // Disparo normal com a tecla espaço, respeitando o fireRate
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            Fire(); // Altera para disparar o projétil normal
            nextFire = Time.time + fireRate;  // Atualiza o tempo para o próximo disparo
        }

        // Disparo do projétil maior com a tecla 'Z' e verificação de cooldown
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Time.time >= lastBigProjectileTime + bigProjectileCooldown)
            {
                FireBigProjectile();
                lastBigProjectileTime = Time.time;  // Atualiza o tempo do último disparo grande
                PlaySound(laserLarge_002);  // Toca o som do disparo grande

                // Atualiza o status do projétil grande para "Carregando..."
                if (bigProjectileStatusText != null)
                {
                    bigProjectileStatusText.text = "Carregando...";
                }
            }
            else
            {
                Debug.Log("Carregando especial...");
            }
        }
    }

    // Atualiza o status do BigProjectile
    private void UpdateBigProjectileStatus()
    {
        if (bigProjectileStatusText != null)
        {
            // Verifica se o BigProjectile está pronto para ser disparado
            if (Time.time >= lastBigProjectileTime + bigProjectileCooldown)
            {
                bigProjectileStatusText.text = "Pressione Z";  // Mostra que está pronto
            }
            else
            {
                bigProjectileStatusText.text = "Carregando";  // Mostra que está em cooldown
            }
        }
    }

    // Implementação da movimentação horizontal
    public void MoveHorizontally(float x)
    {
        float horizontalMovement = x * Time.deltaTime * spaceship.GetSpeed();
        transform.Translate(new Vector3(horizontalMovement, 0, 0));
    }

    // Implementação da movimentação vertical
    public void MoveVertically(float y)
    {
        float verticalMovement = y * Time.deltaTime * spaceship.GetSpeed();
        transform.Translate(new Vector3(0, verticalMovement, 0));
    }

    // Disparo do projétil normal
    public void Fire()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab do projétil normal não atribuído!");
        }
    }

    // Disparo do projétil maior
    public void FireBigProjectile()
    {
        if (bigProjectilePrefab != null)
        {
            Debug.Log("Disparando projétil maior!");
            Instantiate(bigProjectilePrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab do projétil maior não atribuído!");
        }
    }

    // Função para tocar sons
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip de som ou AudioSource não atribuído!");
        }
    }
}
