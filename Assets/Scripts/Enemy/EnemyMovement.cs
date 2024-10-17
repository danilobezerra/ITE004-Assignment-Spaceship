using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovementController
{
    public float fallSpeed = 2f; // Velocidade de queda padrão
    public float bottomLimit = -5f; // Limite inferior da queda

    public AudioClip destructionSound; // Clip de som para a destruição

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
