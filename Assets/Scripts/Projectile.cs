using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Projectile instance;

    public float speed = 25f;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
        Instantiate(Projectile.instance, new Vector3(0, 5, 0), Quaternion.identity);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
