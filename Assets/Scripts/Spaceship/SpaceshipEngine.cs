using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipEngine : MonoBehaviour,
    IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Spaceship spaceship;
    private int ammo;

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
	ammo = 30;
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

	if (Input.GetButtonDown("Fire2")) {
            Reload();
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
	if( ammo > 0 ){
            Instantiate(projectilePrefab,
            	transform.position, Quaternion.identity);
	    ammo--;
	    Debug.Log( "Ammo: " + ammo );
	}
	else
	    Debug.Log( "Out of ammo" );
    }

    public void Reload()
    {
	ammo = 30;
	Debug.Log( "Ammo: " + ammo );
    }
}