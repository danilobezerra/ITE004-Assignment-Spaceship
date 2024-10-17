using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovementController
{
    public float fallSpeed = 2f;
    public float bottomLimit = -5f;
    public AudioClip destructionSound;
    public SpaceshipEngine spaceshipEngine;  // Referência ao script SpaceshipEngine

    void Start()
    {
        spaceshipEngine = FindObjectOfType<SpaceshipEngine>(); // Encontra o SpaceshipEngine na cena
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyEnemy()
    {
        PlayDestructionSound();
        spaceshipEngine.IncreaseScore(); // Aumenta a pontuação ao destruir o inimigo
        Destroy(gameObject);
    }

    private void PlayDestructionSound()
    {
        GameObject soundObject = new GameObject("DestructionSound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = destructionSound;
        audioSource.playOnAwake = false;
        audioSource.Play();
        Destroy(soundObject, destructionSound.length);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            DestroyEnemy();
        }
    }

    public void MoveHorizontally(float x) { }
    public void MoveVertically(float y) { }
}
