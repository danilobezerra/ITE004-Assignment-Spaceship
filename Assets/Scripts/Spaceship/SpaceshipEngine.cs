using System.Collections;
using UnityEngine;

public class SpaceshipEngine : MonoBehaviour,
    IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Spaceship spaceship;
    public int amno = 3;
  

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
    }

    public void Update()
    {
        if (Input.GetButton("Horizontal")) {
            spaceship.MoveHorizontally(Input.GetAxis("Horizontal"));
        }

        if (Input.GetButton("Vertical")) {
            spaceship.MoveVertically(Input.GetAxis("Vertical"));
        }

        if (Input.GetButtonDown("Fire1")) {
            spaceship.ApplyFire();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            spaceship.ApplyPowerFire();
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
        
        
        if(spaceship.maxAmmo < 1 )
        {
            if (!spaceship.recarregando)
            {
                StartCoroutine(spaceship.UseAmmo());
            }
           
        }
        else
        {
            spaceship.maxAmmo--;
            Instantiate(projectilePrefab,
            transform.position, Quaternion.identity);
        }


    }

    public IEnumerator PowerFire()
    {
        if (spaceship.maxAmmo < 1)
        {
            if (!spaceship.recarregando)
            {
                StartCoroutine(spaceship.UseAmmo());
            }

        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                spaceship.maxAmmo--;
                yield return new WaitForSeconds(0.05f);
                Instantiate(projectilePrefab,
            transform.position, Quaternion.identity);
            }
            
        }
    }

}
