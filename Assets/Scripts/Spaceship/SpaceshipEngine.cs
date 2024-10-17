using UnityEngine;
using System.Collections;

public class SpaceshipEngine : MonoBehaviour, IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Spaceship spaceship;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    private void Start() 
    {
        currentAmmo = maxAmmo;
    }

    public IEnumerator Reload()
    {
        isReloading = true;  
        yield return new WaitForSeconds(reloadTime);  
        currentAmmo = maxAmmo;  
        isReloading = false;  
    }

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
    }

    private void Update()
    {

        if (Input.GetButton("Horizontal"))
            spaceship.MoveHorizontally(-Input.GetAxis("Horizontal"));

        if (Input.GetButton("Vertical"))
            spaceship.MoveVertically(-Input.GetAxis("Vertical"));

        if (isReloading) return;

        if (Input.GetButtonDown("Fire1"))
            FireSingleShot();

        if (Input.GetButtonDown("Fire2"))
            FireMultiShot();
    }

    public void MoveHorizontally(float x)
    {
        transform.Translate(new Vector3(Time.deltaTime * x, 0, 0));
    }

    public void MoveVertically(float y)
    {
        transform.Translate(new Vector3(0, Time.deltaTime * y, 0));
    }

    public void FireSingleShot()
    {
        if (currentAmmo > 0)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            currentAmmo--;
        }
        else
        {
             StartCoroutine(Reload());
        }
    }

    public void FireMultiShot()
    {
        if (currentAmmo >= 3)
        {
            for (int i = -1; i <= 1; i++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(i * 0.5f, 0, 0);
                Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            }
            currentAmmo -= 3;
        }
        else
        {
             StartCoroutine(Reload());
        }
    }

    public void Fire()  
    {
        FireMultiShot();
    }
}
