using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25f;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.CompareTag("Enemy"))
        {
            Destroy(colisor.gameObject);
            Destroy(this.gameObject);
        }
    }
}
