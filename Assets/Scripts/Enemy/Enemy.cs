using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public AudioClip explosionSound;  // Efeito sonoro para explosão
    public AudioClip laserSound;      // Efeito sonoro para disparo
    private AudioSource audioSource;  // Referência ao componente AudioSource
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private bool isDead = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Bullet()
    {
        // Cria o projétil e o dispara para baixo
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;

        // Toca o som do disparo
        PlaySound(laserSound);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido é um projétil
        Projectile projectile = collision.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDamage(projectile.damage);
            Destroy(collision.gameObject);   // Destroi o projétil após a colisão
        }

        // Verifica se colidiu com a nave
        Spaceship spaceship = collision.GetComponent<Spaceship>();
        if (spaceship != null)
        {
            spaceship.TakeDamage(1);
            Destroy(gameObject);  // Destroi o inimigo
        }
    }

    public void TakeDamage(int damage)
    {
        // Evita múltiplas execuções se o inimigo já estiver destruído
        if (isDead) return;
        health -= damage;
        if (health <= 0)
        {
            isDead = true;  // Marca o inimigo como destruído
            _animator.SetBool("isExploding", true);  // Inicia a animação de explosão

            // Toca o som de explosão
            PlaySound(explosionSound);

            // Inicia a Coroutine para esperar a animação de explosão antes de destruir o inimigo
            StartCoroutine(WaitForExplosionAnimation());
        }
    }

    private IEnumerator WaitForExplosionAnimation()
    {
        // Espera até a animação de explosão terminar
        yield return new WaitForSeconds(1.5f); // Tempo ajustável conforme a duração da animação

        // Após a animação, destrói o inimigo
        Destroy(gameObject);
    }

    // Função para tocar o áudio
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
