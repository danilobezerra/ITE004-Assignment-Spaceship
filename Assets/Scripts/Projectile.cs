using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float speed = 25f;

    void Update()
    {
        Direcao = this.transform.up;
    }

    public Vector2 Direcao
    {
        set
        {
            this.transform.up = value;
            this.rigidbody2D.velocity = this.transform.up * this.speed;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D  collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.Destruir();
            Destroy(this.gameObject);
        }
    }
}
