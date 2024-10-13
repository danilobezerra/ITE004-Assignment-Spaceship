using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;             
    public GameObject projectilePrefab;  
    public float projectileSpeed = 5f;   

    public void Bullet()
    {
        // Cria o projétil e o dispara para baixo
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido é um projétil
        Projectile projectile = collision.GetComponent<Projectile>();
        BigProjectile bigProjectile2 = collision.GetComponent<BigProjectile>();

        if (bigProjectile2 != null)
        {
            TakeDamage(bigProjectile2.damage);
        }

        if (projectile != null)
        {
            TakeDamage(projectile.damage); 
            Destroy(collision.gameObject);   // Destroi o projétil após a colisão
        }

        // Verifica se colidiu com a nave
        Spaceship spaceship = collision.GetComponent<Spaceship>();
        if (spaceship != null)
        {
            spaceship.TakeDamage(1); 
            Destroy(gameObject);       
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; 
        if (health <= 0)
        {
            Destroy(gameObject);  // Destroi o inimigo se a vida chegar a 0
        }
    }
}  