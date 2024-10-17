using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 25f;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.down);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if( collider.CompareTag("player") ){
            Spaceship spaceship = collider.GetComponent<Spaceship>();
            spaceship.Damage();
            Destroy(this.gameObject);
        }

    }
}
