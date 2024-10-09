using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 25f;
    public int damage = 1;  // Adiciona o campo 'damage' que será usado para causar dano

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
