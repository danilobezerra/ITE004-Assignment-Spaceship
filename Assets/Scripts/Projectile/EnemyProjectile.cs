using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f; 
    public int damage = 1;   
    private Vector2 direction;  // Direção do projétil

    void Update()
    {
        // Move o projétil na direção definida
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // Método para definir a direção do projétil
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    // Verifica colisão com a Spaceship
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido é a nave
        Spaceship spaceship = collision.GetComponent<Spaceship>();
        
        if (spaceship != null)
        {
            spaceship.TakeDamage(damage);  
            Destroy(gameObject);           
        }
    }

    // Método para destruir o projétil após um tempo (opcional)
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroi o projétil se sair da tela
    }
}
