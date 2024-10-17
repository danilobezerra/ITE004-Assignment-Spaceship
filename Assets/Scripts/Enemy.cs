using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private int ammo;
    private float burstCooldown;
    private bool burstOn;
    private Bounds _cameraBounds;
    private SpriteRenderer _spriteRenderer;
    private AudioSource audioSource;

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

    public bool nearHorBound(){
	var spriteWidth = _spriteRenderer.sprite.bounds.extents.x;
	return transform.position.x > _cameraBounds.max.x - spriteWidth/2
		|| transform.position.x < _cameraBounds.min.x + spriteWidth/2;
    }
    public bool nearVerBound(){
	var spriteHeight = _spriteRenderer.sprite.bounds.extents.y;
	return transform.position.y > _cameraBounds.max.y - spriteHeight/2
		|| transform.position.y < _cameraBounds.min.y + spriteHeight/2;
    }

    void Start() {
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Camera.main.aspect;
        var size = new Vector3(width, height);
        ammo = 30;
        burstCooldown = 0f;
        burstOn = false;
        _cameraBounds = new Bounds(Vector3.zero, size);

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate() {
        if( burstCooldown > 0F ){
            burstCooldown -= Time.deltaTime;
        }

        if( burstOn && burstCooldown <= 0.0F && ammo > 0 ){
            ammo--;
            _gunController.Fire();
       
            burstCooldown = 0.08F;
            Debug.Log( "Ammo: " + ammo + "  Burst: " + burstOn );
        }
    }

}
