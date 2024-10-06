using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed;

    public int maxAmmo = 10;
    private int currentAmmo;
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
        // TODO: Controlar velocidade com base no estado da nave
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
