using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpaceshipEngine : MonoBehaviour, IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Spaceship spaceship;
    public Text scoreText;
    public Text ammoText;
    public GameObject infoText;

    private int currentAmmo = 20; 
    private int maxAmmo = 20;    
    private int score = 0;       

    private bool isFiringLeft = false;
    private bool isFiringRight = false;
    private Coroutine firingCoroutine;

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
        UpdateUI();
        infoText.SetActive(false); 
    }

    public void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(0) && !isFiringLeft)
        {
            FireInDirection(Vector3.up); 
        }

        if (Input.GetMouseButton(0) && !isFiringLeft)
        {
            isFiringLeft = true;
            StartCoroutine(FireBurst(5));
        }
        if (Input.GetMouseButtonUp(0))
        {
            isFiringLeft = false;
        }

        if (Input.GetMouseButton(1) && !isFiringRight)
        {
            isFiringRight = true;
            firingCoroutine = StartCoroutine(FireContinuously(Vector3.up));
        }
        if (Input.GetMouseButtonUp(1) && isFiringRight)
        {
            isFiringRight = false;
            StopCoroutine(firingCoroutine); 
        }

        if (Input.GetMouseButtonDown(2))
        {
            FireInDirection(Vector3.right);
            FireInDirection(Vector3.left);  
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(20); 
        }

        if (currentAmmo <= 0)
        {
            infoText.SetActive(true); 
        }
        else
        {
            infoText.SetActive(false); 
        }
    }

    private void HandleMovement()
    {
        if (Input.GetButton("Horizontal"))
        {
            spaceship.MoveHorizontally(Input.GetAxis("Horizontal"));
        }
        if (Input.GetButton("Vertical"))
        {
            spaceship.MoveVertically(Input.GetAxis("Vertical"));
        }
    }

    public void Fire()
    {
        FireInDirection(Vector3.up);
    }

    public void FireMultiple(int shots)
    {
        for (int i = 0; i < shots && currentAmmo > 0; i++)
        {
            FireInDirection(Vector3.up); 
        }
        UpdateUI(); 
    }

    public void FireInDirection(Vector3 direction)
    {
        if (currentAmmo > 0)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetDirection(direction);
            currentAmmo--;
            UpdateUI();
        }
    }

    public bool Reload(int ammo)
    {
        if (currentAmmo < maxAmmo)
        {
            int amountToReload = Mathf.Min(ammo, maxAmmo - currentAmmo);
            currentAmmo += amountToReload;
            UpdateUI(); 
            return true;
        }
        return false;
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

    private IEnumerator FireBurst(int shots)
    {
        for (int i = 0; i < shots && currentAmmo > 0; i++)
        {
            FireInDirection(Vector3.up); 
            yield return new WaitForSeconds(0.1f); 
        }
        isFiringLeft = false;
        UpdateUI(); 
    }

    private IEnumerator FireContinuously(Vector3 direction)
    {
        while (true)
        {
            FireInDirection(direction); 
            yield return new WaitForSeconds(0.2f); 
            UpdateUI(); 
        }
    }

    private void UpdateUI()
    {
        scoreText.text = "Pontuação: " + score;
        ammoText.text = "Munição: " + currentAmmo;
    }

    public void IncreaseScore()
    {
        score++;
        UpdateUI();
    }
}
