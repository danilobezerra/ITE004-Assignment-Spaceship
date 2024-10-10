using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipEngine : MonoBehaviour,
    IMovementController, IGunController
{
    private float intervaloTiro;
    public float cooldownTiro;
    public Projectile projectilePrefab;
    public Spaceship spaceship;


     void Start()
    {
        this.intervaloTiro = 0;
    }

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
    }

    public void Update()
    {
        this.intervaloTiro += Time.deltaTime;
        if (this.intervaloTiro >= this.cooldownTiro)
        {
            this.intervaloTiro = 0;
            Atirar();
        }

        if (Input.GetButton("Horizontal")) {
            spaceship.MoveHorizontally(Input.GetAxis("Horizontal"));
        }

        if (Input.GetButton("Vertical")) {
            spaceship.MoveVertically(Input.GetAxis("Vertical"));
        }
        void Atirar()
        {
            if (Input.GetButton("Fire1")) {
            spaceship.ApplyFire();
            }
        }
    }

    public void MoveHorizontally(float x)
    {
        var horizontal = Time.deltaTime * x;
        transform.Translate(new Vector3(horizontal, 0));
    }

    public void MoveVertically(float y)
    {
        var vertical = Time.deltaTime * y;
        transform.Translate(new Vector3(0, vertical));
    }

    public void Fire()
    {
        Instantiate(projectilePrefab,
            transform.position, Quaternion.identity);
    }
}
