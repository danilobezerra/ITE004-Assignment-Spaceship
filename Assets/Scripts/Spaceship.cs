using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float speed;
    private int ammo;
    private float burstCooldown;
    private bool burstOn;
    private int health;

    private AudioSource audioSource;

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
	if( ammo > 0 ){
	    ammo--;
        audioSource.Play();
            _gunController.Fire();
	}
	Debug.Log( "Ammo: " + ammo + "  Burst: " + burstOn );
    }

    public void SwitchBurstFireOn()
    {
	burstOn = true;
	Debug.Log( "Ammo: " + ammo + "  Burst: " + burstOn );
    }

    public void SwitchBurstFireOff()
    {
	burstOn = false;
	Debug.Log( "Ammo: " + ammo + "  Burst: " + burstOn );
    }

    public void Reload()
    {
	ammo = 30;
        Debug.Log( "Ammo: " + ammo + "  Burst: " + burstOn );
    }

    public float GetSpeed()
    {
        // TODO: Controlar velocidade com base no estado da nave
        return speed;
    }  

    void Start() {
        var height = Camera.main.orthographicSize * 2f;
        audioSource = GetComponent<AudioSource>();
        var width = height * Camera.main.aspect;
        var size = new Vector3(width, height);
	ammo = 30;
	burstCooldown = 0.0f;
	burstOn = false;
        _cameraBounds = new Bounds(Vector3.zero, size);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        health = 10;
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

        if( burstCooldown > 0.0F ){
            burstCooldown -= Time.deltaTime;
        }

        if( burstOn && burstCooldown <= 0.0F && ammo > 0 ){
            ammo--;
            _gunController.Fire();
            audioSource.Play();
            burstCooldown = 0.08F;
            Debug.Log( "Ammo: " + ammo + "  Burst: " + burstOn );
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if( collider.CompareTag("enemy") ){
            Enemy enemy = collider.GetComponent<Enemy>();
            Destroy(enemy.gameObject);
            Destroy(this.gameObject);
        }

    }

    public void Damage(){
        health--;
        speed -= 0.5F;
        Debug.Log("health:"+health);
        if( health <= 0 )
            Destroy(this.gameObject);
    }
}
