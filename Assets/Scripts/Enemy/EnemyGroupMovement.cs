using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupMovement : MonoBehaviour
{
    public float moveSpeed = 2f;    // Velocidade de movimento horizontal
    public float moveDownDistance = 0.5f;  // Distância que os inimigos descem ao mudar de direção
    public float boundaryX = 7f;    // Limite da tela (direita/esquerda)

    private bool movingRight = true;   // Direção inicial (começa movendo para a direita)

    void Update()
    {
        MoveEnemies();
    }

    void MoveEnemies()
    {
        // Movimenta o grupo de inimigos horizontalmente
        float horizontalMovement = moveSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(horizontalMovement, 0, 0);

        // Verifica se o grupo atingiu a borda da tela
        if (movingRight && transform.position.x >= boundaryX)
        {
            ChangeDirection();
        }
        else if (!movingRight && transform.position.x <= -boundaryX)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        // Altera a direção do movimento
        movingRight = !movingRight;

        // Desce o grupo de inimigos
        transform.Translate(0, -moveDownDistance, 0);
    }
}