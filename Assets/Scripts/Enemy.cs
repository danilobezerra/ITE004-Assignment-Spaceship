using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float minSpeed;
    public float maxSpeed;
    private float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        this.ySpeed = Random.Range(this.minSpeed, this.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        this.rigidbody.velocity = new Vector2(0, -this.ySpeed);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void Destruir()
    {
        ControladorPontuacao.Pontuacao++;
        Destroy(this.gameObject);
    }
}
