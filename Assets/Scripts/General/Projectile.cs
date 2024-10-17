using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25f;
    private Vector3 direction;

    public AudioClip shootSound; 
    private AudioSource audioSource;

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
            Destroy(other.gameObject); 
            Destroy(gameObject); 
        }
    }
}
