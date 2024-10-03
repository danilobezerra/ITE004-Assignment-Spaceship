using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiShotController : MonoBehaviour 
{
    public float speed = 25f;

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
