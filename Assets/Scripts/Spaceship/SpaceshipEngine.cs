using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipEngine : MonoBehaviour,
    IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Spaceship spaceship;
    private bool isTripleShot = false; // Inicia no modo de tiro simples
    private int maxAmmo = 15; // Quantidade máxima de tiros antes de precisar recarregar
    private int currentAmmo;  // Tiros disponíveis atualmente 
    private bool isReloading = false; // Se está recarregando

    void Start()
{
    currentAmmo = maxAmmo; // Inicia com munição cheia
}

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

    // Alternar entre tiro simples e triplo ao pressionar F
    if (Input.GetKeyDown(KeyCode.F)) {
        isTripleShot = !isTripleShot; // Alterna entre os modos
        Debug.Log(isTripleShot ? "Tiro triplo ativado" : "Tiro simples ativado");
    }

    // Recarregar ao pressionar R
    if (Input.GetKeyDown(KeyCode.R) && !isReloading) {
        StartCoroutine(Reload());
    }
}
    IEnumerator Reload()
{
    isReloading = true;
    Debug.Log("Recarregando...");
    
    yield return new WaitForSeconds(2); // Simula o tempo de recarga (2 segundos, por exemplo)

    currentAmmo = maxAmmo; // Munição é restaurada
    isReloading = false;
    
    Debug.Log("Recarga completa! Munição: " + currentAmmo);
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
    if (isReloading || currentAmmo <= 0) {
        Debug.Log("Sem munição! Recarregue.");
        return; // Não pode atirar se está sem munição ou recarregando
    }

    if (isTripleShot && currentAmmo >= 3)
    {
        // Tiro triplo consome 3 de munição
        Instantiate(projectilePrefab, transform.position, Quaternion.identity); // Disparo central
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -30)); // Disparo à direita
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 30)); // Disparo à esquerda
        currentAmmo -= 3; // Reduz a munição em 3
    }
    else if (!isTripleShot && currentAmmo >= 1)
    {
        // Tiro simples consome 1 de munição
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        currentAmmo -= 1; // Reduz a munição em 1
    }

    // Exibe munição atual no console (opcional)
    Debug.Log("Munição restante: " + currentAmmo);
}



}
