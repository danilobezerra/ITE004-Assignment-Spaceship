using UnityEngine;
using UnityEngine.SceneManagement;
public class Spaceship : MonoBehaviour
{
    public float speed;
    public int maxAmmo = 10;
    private int currentAmmo;
    public int health = 3;  // Quantidade de vidas da nave
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
        if (currentAmmo > 0)
        {
            _gunController.Fire();
            currentAmmo--; 
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
        health -= damage; // Reduz a vida da nave
        Debug.Log("A nave foi atingida! Vida restante: " + health);
        speed *= 0.7f;
        
        if (health <= 0)
        {
            Debug.Log("A nave foi destruída!");
            Destroy(gameObject);  // Destroi a nave se a vida chegar a 0
            RestartGame();
        }
    }

    void Start() {
        currentAmmo = maxAmmo;
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        var size = new Vector3(width, height);
        _cameraBounds = new Bounds(Vector3.zero, size);

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        // Verifica se a tecla R foi pressionada para recarregar
        if (Input.GetKeyDown(KeyCode.R)) {
            Reload(); // Chama a função de recarga
        }
    }

    void LateUpdate() {
        var newPosition = transform.position;

        var spriteWidth = _spriteRenderer.sprite.bounds.extents.x;
        var spriteHeight = _spriteRenderer.sprite.bounds.extents.y;

        newPosition.x = Mathf.Clamp(transform.position.x,
            _cameraBounds.min.x + spriteWidth, _cameraBounds.max.x - spriteWidth);
        newPosition.y = Mathf.Clamp(transform.position.y,
            _cameraBounds.min.y + spriteHeight, _cameraBounds.max.y - spriteHeight);

        transform.position = newPosition;
    }
}
