using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importar para usar TMP_Text

public class SpaceshipEngine : MonoBehaviour,
    IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Spaceship spaceship;
    public AudioClip threeWaySound;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    private AudioSource _audioSource;
    private bool isTripleShot = false;
    private int maxAmmo = 45;
    private int currentAmmo;
    private bool isReloading = false;

    // Adiciona uma referência para o texto de munição com TextMeshPro
    public TMP_Text ammoText;

    void Start()
    {
        currentAmmo = maxAmmo;
        _audioSource = GetComponent<AudioSource>();
        UpdateAmmoText(); // Atualiza o texto no início
    }

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
    }

    public void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            spaceship.MoveHorizontally(Input.GetAxis("Horizontal"));
        }

        if (Input.GetButton("Vertical"))
        {
            spaceship.MoveVertically(Input.GetAxis("Vertical"));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo != 0 && !isReloading)
            {
                if (!isTripleShot)
                {
                    _audioSource.PlayOneShot(shootingSound);
                }
                else
                {
                    _audioSource.PlayOneShot(threeWaySound);
                }
                spaceship.ApplyFire();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isTripleShot = !isTripleShot;
            Debug.Log(isTripleShot ? "Tiro triplo ativado" : "Tiro simples ativado");
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        _audioSource.PlayOneShot(reloadingSound);
        Debug.Log("Recarregando...");

        yield return new WaitForSeconds(2);

        currentAmmo = maxAmmo;
        isReloading = false;

        Debug.Log("Recarga completa! Munição: " + currentAmmo);
        UpdateAmmoText(); // Atualiza o texto após a recarga
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
        if (isReloading || currentAmmo <= 0)
        {
            UpdateAmmoText(); // Atualiza o texto se não houver munição
            return;
        }

        if (isTripleShot && currentAmmo >= 3)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -30));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 30));
            currentAmmo -= 3;
        }
        else if (!isTripleShot && currentAmmo >= 1)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            currentAmmo -= 1;
        }

        UpdateAmmoText(); // Atualiza o texto após disparar
    }

    // Método para atualizar o texto de munição na tela
    private void UpdateAmmoText()
    {
        ammoText.text = "Munição: " + currentAmmo; // Atualiza o texto
    }
}
