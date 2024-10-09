using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;  // Vida do inimigo

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido é um projétil
        Projectile projectile = collision.GetComponent<Projectile>();

        if (bigProjectile2 != null)
        {
            TakeDamage(bigProjectile2.damage);
            Destroy(collision.gameObject);
        }
        
        if (projectile != null)
        {
            TakeDamage(projectile.damage);  // Aplica o dano do projétil ao inimigo
            Destroy(collision.gameObject);  // Destroi o projétil após a colisão
        }

        // Verifica se colidiu com a nave (Spaceship)
        Spaceship spaceship = collision.GetComponent<Spaceship>();
        if (spaceship != null)
        {
            spaceship.TakeDamage(1);  // Aplica 1 de dano à nave
            Destroy(gameObject);      // Destroi o inimigo após a colisão
        }
    }

    // Método para aplicar dano ao inimigo
    public void TakeDamage(int damage)
    {
        health -= damage;  // Reduz a vida do inimigo
        if (health <= 0)
        {
            Destroy(gameObject);  // Destroi o inimigo se a vida chegar a 0
        }
    }
}