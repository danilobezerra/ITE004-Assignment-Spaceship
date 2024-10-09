using UnityEngine;

public class BigProjectile : MonoBehaviour
{
    public float speed = 25f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

