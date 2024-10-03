using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
        
        if(transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }
    
}
