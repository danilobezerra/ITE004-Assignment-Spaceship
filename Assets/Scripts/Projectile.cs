using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 25f;
    public Spawner spawner;
    private AudioSource audioSource;
    public AudioClip explosionClip;
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }


    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if( collider.CompareTag("enemy") ){
            AudioSource.PlayClipAtPoint(explosionClip, transform.position);
            Enemy enemy = collider.GetComponent<Enemy>();
            Destroy(enemy.gameObject);
            Destroy(this.gameObject);
        }

    }
}
