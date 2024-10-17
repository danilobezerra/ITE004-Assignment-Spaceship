using UnityEngine;

public class ShootingController : MonoBehaviour, IGunController
{
    public Projectile projectilePrefab; 
    public float fireRate = 0.2f; // Tempo entre disparos
    private float nextFireTime;

    void Update()
    {
        // Disparo normal
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FireNormal();
        }

        
        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextFireTime)// aperta x que sai 2 projetil
        {
            nextFireTime = Time.time + fireRate;
            FireCone();
        }
    }

    public void FireNormal()
    {
        // Dispara um projétil
        if (projectilePrefab != null) 
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Projectile prefab is not assigned in the ShootingController!");
        }
    }

    public void FireCone()
    {
        // Dispara em padrão cônico
        float angleStep = 15f; 
        float angle = (2 - 1) * angleStep / 2; 

        for (int i = 0; i < 2; i++) // Agora disparará 2 projéteis
        {
            //nao botei fe no tempo que ia levar 
            Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);
            Projectile projectile = Instantiate(projectilePrefab, transform.position, projectileRotation); 
            projectile.transform.up = projectileRotation * Vector3.up; 
            angle -= angleStep; 
        }
    }

    public void Fire()
    {
        FireNormal(); 
    }
}
