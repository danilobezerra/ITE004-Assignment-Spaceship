using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmaBasica : MonoBehaviour, IGunController
{
    public Spaceship spaceship;

    public Projectile projectilePrefab;
    private float intervaloTiro;
    public float cooldownTiro;
    public Transform[] gunPosition;


    // Start is called before the first frame update
    public virtual void  Start()
    {
        this.intervaloTiro = 0;

    }

    public void OnEnable()
    {
        spaceship.SetGunController(this);
    }

    // Update is called once per frame
    void Update()
    {
        this.intervaloTiro += Time.deltaTime;
        if (this.intervaloTiro >= this.cooldownTiro)
        {
            this.intervaloTiro = 0;
            Atirar();
        }
    }

    void Atirar()
    {
        if (Input.GetButton("Fire1"))
        {
            spaceship.ApplyFire();
        }
    }

    protected void CriarTiro(Vector2 position)
    {
        Instantiate(projectilePrefab, position, Quaternion.identity);
    }

    public abstract void Fire();

    public void Ativar()
    {
        this.gameObject.SetActive(true);
    }
    public void Desativar()
    {
        this.gameObject.SetActive(false);
    }
}

