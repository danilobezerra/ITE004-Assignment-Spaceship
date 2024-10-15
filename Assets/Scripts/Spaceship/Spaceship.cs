using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro; 

public class Spaceship : MonoBehaviour
{
    public float speed;
    public int maxAmmo = 10;
    private int currentAmmo = 10;
    public int health = 3;  // Quantidade de vidas da nave
    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;

    private IMovementController _movementController;
    private IGunController _gunController;
    private Animator _animator;  // Referência ao Animator

    public AudioClip fireSound;      // Som de tiro
    public AudioClip damageSound;    // Som de dano
    public AudioClip explosionSound; // Som de explosão
    private AudioSource audioSource; // Referência ao componente AudioSource
    public TextMeshProUGUI healthStatus;

    private bool isDead = false;  // Variável para verificar se a nave já foi destruída

    public void SetMovementController(IMovementController movementController)
    {
        _movementController = movementController;
    }

    public void SetGunController(IGunController gunController)
    {
        _gunController = gunController;
    }

    public void MoveHorizontally(float x)
    {
        _movementController.MoveHorizontally(x * GetSpeed());
    }

    public void MoveVertically(float y)
    {
        _movementController.MoveVertically(y * GetSpeed());
    }

    public void ApplyFire()
    {
        if (currentAmmo > 0)
        {
            _gunController.Fire();
            currentAmmo--;
            PlaySound(fireSound);
        }
        else
        {
            Debug.Log("Sem munição! Por favor, recarregue...");
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void Reload()
    {
        currentAmmo = maxAmmo; // Recarrega a munição
        Debug.Log("Munição recarregada!"); // Mensagem opcional
    }

    public int GetCurrentAmmo() // Adicionando um getter
    {
        return currentAmmo;
    }

    public void RestartGame()
    {
        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Função para reduzir a vida da nave e aplicar o dano
    public void TakeDamage(int damage)
    {
        // Evita tomar dano se já está morto
        if (isDead) return;

        health -= damage; // Reduz a vida da nave
        Debug.Log("A nave foi atingida! Vida restante: " + health);
        healthStatus.text = "| Vidas: " + health;
        speed *= 0.7f;

        // Toca o som de dano
        PlaySound(damageSound);

        if (health <= 0 && !isDead)
        {
            Debug.Log("A nave foi destruída!");
            isDead = true;  // Marca a nave como destruída

            // Define o parâmetro bool no Animator para iniciar a animação de explosão
            _animator.SetBool("isExploding", true);
            StartCoroutine(WaitForExplosionAnimation());
            PlaySound(explosionSound);
        }
    }

    // Coroutine para esperar a animação de explosão terminar antes de reiniciar o jogo
    private IEnumerator WaitForExplosionAnimation()
    {
        // Espera até a animação de explosão terminar
        yield return new WaitForSeconds(1.5f); // Tempo ajustável conforme a duração da animação

        // Após a animação, reinicia o jogo
        RestartGame();
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        var size = new Vector3(width, height);
        _cameraBounds = new Bounds(Vector3.zero, size);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Pegar a referência ao Animator
        _animator = GetComponent<Animator>();

        // Garante que a nave não comece já na animação de explosão
        _animator.SetBool("isExploding", false);

        // Pega o componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verifica se a tecla R foi pressionada para recarregar
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(); // Chama a função de recarga
        }
    }

    void LateUpdate()
    {
        var newPosition = transform.position;

        var spriteWidth = _spriteRenderer.sprite.bounds.extents.x;
        var spriteHeight = _spriteRenderer.sprite.bounds.extents.y;

        newPosition.x = Mathf.Clamp(transform.position.x,
            _cameraBounds.min.x + spriteWidth, _cameraBounds.max.x - spriteWidth);
        newPosition.y = Mathf.Clamp(transform.position.y,
            _cameraBounds.min.y + spriteHeight, _cameraBounds.max.y - spriteHeight);

        transform.position = newPosition;
    }

    // Função para tocar sons
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
