using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotController : MonoBehaviour 
{
    public float speed = 25f;
    public SpaceshipEngine spaceshipEngine;

    private void Start() 
    {
        spaceshipEngine = GetComponent<SpaceshipEngine>();
    }

    public void FireMultiShot() 
    {
        if (spaceshipEngine.currentAmmo >= 3) 
        {
            for (int i = -1; i <= 1; i++) 
            {
                Vector3 spawnPosition = transform.position + new Vector3(i * 0.5f, 0, 0);
                Instantiate(spaceshipEngine.projectilePrefab, spawnPosition, Quaternion.identity);
            }
            spaceshipEngine.currentAmmo -= 3;
        } 
        else 
        {
            spaceshipEngine.Reload();
        }
    }

    public void FireSingleShot() 
    {
        if (spaceshipEngine.currentAmmo >= 1) 
        {
            Instantiate(spaceshipEngine.projectilePrefab, transform.position, Quaternion.identity);
            spaceshipEngine.currentAmmo--;
        } 
        else 
        {
            spaceshipEngine.Reload();
        }
    }

    private void Update() 
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }

    private void OnBecameInvisible() 
    {
        Destroy(gameObject);
    }
}
