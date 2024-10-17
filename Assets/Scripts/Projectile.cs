using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25f;
    private Vector3 direction;

    public AudioClip shootSound; // Clip de som para o tiro
    private AudioSource audioSource; // Componente de AudioSource

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    internal void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        // Toca o som do tiro
        audioSource.clip = shootSound;
        audioSource.Play();
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * direction);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destrói o inimigo
            Destroy(gameObject); // Destroi o projétil
        }
    }
}
