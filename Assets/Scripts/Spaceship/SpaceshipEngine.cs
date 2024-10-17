using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipEngine : MonoBehaviour, IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Spaceship spaceship;
    private int currentAmmo = 10; // Munição atual
    private int maxAmmo = 10; // Munição máxima

    public void OnEnable()
    {
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
    }

    public void Update()
    {
        HandleMovement();
        HandleShooting();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(10); // Recarregar com 10 unidades de munição
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

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse
        {
            FireInDirection(Vector3.left); // Disparo para a esquerda
        }
        if (Input.GetMouseButtonDown(1)) // Botão direito do mouse
        {
            FireInDirection(Vector3.right); // Disparo para a direita
        }
        if (Input.GetMouseButtonDown(2)) // Botão do meio do mouse
        {
            FireMultipleProjectiles(); // Dispara múltiplos projéteis
        }
    }

    public void Fire()
    {
        FireInDirection(Vector3.right); // Exemplo: dispara para a direita
    }

    public void FireMultiple(int shots)
    {
        for (int i = 0; i < shots && currentAmmo > 0; i++)
        {
            FireInDirection(Vector3.right); // Ajustar a direção se necessário
        }
    }

    public void FireInDirection(Vector3 direction)
    {
        if (currentAmmo > 0)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().SetDirection(direction);
            currentAmmo--;
        }
    }

    public void FireMultipleProjectiles()
    {
        const int totalShots = 5; // Número total de projéteis a disparar
        float baseSpeed = 30f; // Aumente a velocidade base

        for (int i = 0; i < totalShots; i++)
        {
            if (currentAmmo > 0)
            {
                // Calcular a direção (para cima e para baixo)
                Vector3 direction = (i % 2 == 0) ? Vector3.up : Vector3.down;

                // Criar o projétil
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetDirection(direction);

                // Ajustar a velocidade do projétil
                projectile.speed = baseSpeed - (i * 5); // Velocidade decrescente (pode ajustar o decremento)

                currentAmmo--;
            }
        }
    }

    public bool Reload(int ammo)
    {
        if (currentAmmo < maxAmmo)
        {
            int amountToReload = Mathf.Min(ammo, maxAmmo - currentAmmo);
            currentAmmo += amountToReload;
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
}
