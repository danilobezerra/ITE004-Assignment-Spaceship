using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed;
    public float maxHealth = 3;
    public int maxAmmo = 3;
    public float currentHealth;
    public bool recarregando;

    // Música
    public AudioClip explosaoClip;

    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;

    private IMovementController _movementController;
    private IGunController _gunController;

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
        _gunController.Fire();
    }

    public void ApplyPowerFire()
    {
        StartCoroutine(_gunController.PowerFire());
    }

    public float GetSpeed()
    {
        if (currentHealth == 3)
        {
            return speed;
        }
        else if (currentHealth == 2)
        {
            return speed - 2;
        }
        else
        {
            return speed - 5;
        }
    }

    public IEnumerator UseAmmo()
    {
        recarregando = true;
        yield return new WaitForSeconds(2);
        Recharge();
        recarregando = false;
    }

    public void Recharge()
    {
        maxAmmo = 3;
    }

    public void LoseHealth()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!"); // Verifique se este log aparece
        StartCoroutine(PlayExplosaoAndDestroy());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            LoseHealth();
        }
    }

    void Start()
    {
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        var size = new Vector3(width, height);
        currentHealth = maxHealth;
        _cameraBounds = new Bounds(Vector3.zero, size);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
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

    private IEnumerator PlayExplosaoAndDestroy()
    {
        Debug.Log("Tocando explosão!"); // Verifique se este log aparece
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = explosaoClip; // Define o clip de explosão
        audioSource.Play();

        // Espera o áudio tocar completamente
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject); // Destroi o objeto após a explosão
    }
}
