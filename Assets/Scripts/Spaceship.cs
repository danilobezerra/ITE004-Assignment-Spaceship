using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed;

    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;

    private IMovementController _movementController;
    private IGunController _gunController;
    public GunController GunController;

    private int vidas;

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
        // TODO: Recarregar
        _gunController.Fire();
    }

    public float GetSpeed()
    {
        // TODO: Controlar velocidade com base no estado da nave
        return speed;
    }
    void Start()
    {
        this.vidas = 5;
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        var size = new Vector3(width, height);
        _cameraBounds = new Bounds(Vector3.zero, size);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        EquipAlterGun();

        ControladorPontuacao.Pontuacao = 0;
    }

    public void EquipAlterGun()
    {
        this.GunController.EquipAlterGun();
    }

    public void EquipConeGun()
    {
        this.GunController.EquipConeGun();
    }

    void ColetarPowerUp(ColectPower powerUp)
    {
        PowerEffect powerEffect = powerUp.PowerEffect;
        powerEffect.Aplicar(this);
        powerUp.Coletar();
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Vida--;
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.Destruir();
        } else if (collider.CompareTag("PowerUp"))
        {
            ColectPower powerUp = collider.GetComponent<ColectPower>();
            ColetarPowerUp(powerUp);
        }
    }

    public int Vida {
        get {
            return this.vidas;
        }
        set
        {
            this.vidas = value;
            if (this.vidas < 0){
                this.vidas = 0;
            }
        }
    }
}
