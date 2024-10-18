using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float speed = 25f;
    public GameObject explosao;

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
            GameObject preFab = Instantiate(explosao, new Vector3(this.gameObject.transform.position.x,
            this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            Destroy(preFab.gameObject, 2f);
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.Destruir();
            Destroy(this.gameObject);
        }
    }
}
