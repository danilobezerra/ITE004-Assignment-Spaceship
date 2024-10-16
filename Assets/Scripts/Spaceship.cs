using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed;
    public float damagedMultiplier = 0.5f;
    public float idleSpeed = 0.2f;

    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;

    private IMovementController _movementController;
    private IGunController _gunController;

    private SpaceshipState _currentState = SpaceshipState.Normal;

    public void SetMovementController(IMovementController movementController)
    {
        _movementController = movementController;
    }

    public void SetGunController(IGunController gunController)
    {
        _gunController = gunController;
    }

    public void SetState(SpaceshipState newState)
    {
        _currentState = newState;
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

    public float GetSpeed()
    {
        switch (_currentState)
        {
            case SpaceshipState.Normal:
                return speed;
            case SpaceshipState.Damaged:
                return speed * damagedMultiplier;
            case SpaceshipState.Idle:
                return idleSpeed;
            default:
                return speed;
        }
    }

    void Start()
    {
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        var size = new Vector3(width, height);
        _cameraBounds = new Bounds(Vector3.zero, size);

        _spriteRenderer = GetComponent<SpriteRenderer>();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") == collision.gameObject.CompareTag("Player"))
        {
            SetState(SpaceshipState.Damaged);
            Debug.Log("Nave colidiu com o inimigo! Estado alterado para 'Damaged'.");
        }
    }
}

public enum SpaceshipState
{
    Normal,
    Damaged,
    Idle
}