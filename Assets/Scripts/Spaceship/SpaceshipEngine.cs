using UnityEngine;

public class SpaceshipEngine : MonoBehaviour, IMovementController, IGunController
{
    public Projectile projectilePrefab;      // Projétil normal
    public BigProjectile bigProjectilePrefab;   // Projétil maior
    public Spaceship spaceship;

    public float bigProjectileCooldown = 45f; // Tempo de cooldown para o projétil maior
    private float lastBigProjectileTime;      // Tempo do último disparo do projétil maior

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
    }

    public void Update()
    {
        // Movimentação
        if (Input.GetButton("Horizontal"))
        {
            spaceship.MoveHorizontally(Input.GetAxis("Horizontal"));
        }

        if (Input.GetButton("Vertical"))
        {
            spaceship.MoveVertically(Input.GetAxis("Vertical"));
        }

        // Disparo normal com a tecla espaço
        if (Input.GetButtonDown("Fire1"))
        {
            spaceship.ApplyFire();
        }

        // Disparo do projétil maior com a tecla 'Z' e verificação de cooldown
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= lastBigProjectileTime + bigProjectileCooldown)
        {
            FireBigProjectile();
            lastBigProjectileTime = Time.time; // Atualiza o tempo do último disparo
        }
        if (Input.GetKeyDown(KeyCode.Z) && Time.time < bigProjectileCooldown) 
        {
              Debug.Log("Carregando especial...");
        }
    }

    public void MoveHorizontally(float x)
    {
        var horizontal = Time.deltaTime * x;
        transform.Translate(new Vector3(horizontal, 0));
    }

    public void MoveVertically(float y)
    {
        var vertical = Time.deltaTime * y;
        transform.Translate(new Vector3(0, vertical));
    }

    public void Fire()
    {
        // Disparo do projétil normal
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    public void FireBigProjectile()
    {
        // Verifica se o prefab do projétil maior está atribuído
        if (bigProjectilePrefab != null)
        {
            Debug.Log("Disparando projétil maior!");
            Instantiate(bigProjectilePrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab do projétil maior não atribuído!");
        }
    }
}
