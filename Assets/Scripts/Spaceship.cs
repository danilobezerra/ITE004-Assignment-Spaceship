using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed;
    public float damagedMultiplier = 0.8f;
    public float idleSpeed = 0.6f;

    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;

    //public Sprite damagedSprite;

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
        ChangeColorBasedOnState();  // Muda a cor sempre que o estado mudar
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
                return speed * idleSpeed;
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
        ChangeColorBasedOnState();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            switch (_currentState)
            {
                case SpaceshipState.Normal:
                    SetState(SpaceshipState.Damaged);
                    Debug.Log("Nave colidiu com o inimigo! Estado alterado para 'Damaged'.");
                    break;

                case SpaceshipState.Damaged:
                    SetState(SpaceshipState.Idle);
                    Debug.Log("Nave colidiu com o inimigo! Estado alterado para 'Critical'.");
                    break;

                case SpaceshipState.Idle:
                    Destroy(gameObject);
                    Debug.Log("Nave colidiu com o inimigo! A Nave explodiu.");
                    break;
            }
        }
    }

    void ChangeColorBasedOnState()
    {
        switch (_currentState)
        {
            case SpaceshipState.Normal:
                _spriteRenderer.color = Color.white;
               // _spriteRenderer.sprite = damagedSprite;

                break;

            case SpaceshipState.Damaged:
                _spriteRenderer.color = Color.grey;
                break;

            case SpaceshipState.Idle:
                _spriteRenderer.color = Color.red;
                break;
        }
    }
}

public enum SpaceshipState
{
    Normal,
    Damaged,
    Idle
}