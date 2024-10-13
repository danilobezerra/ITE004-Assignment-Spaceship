using System.Collections;
using UnityEngine;

public class EnemyGroupMovement : MonoBehaviour
{
    public float moveSpeed = 2f;       // Velocidade de movimento horizontal
    public float moveDownDistance = 0.5f;  // Distância que os inimigos descem ao mudar de direção
    public float boundaryX = 7f;       // Limite da tela (direita/esquerda)
    public float descendDuration = 0.5f; // Tempo para descer suavemente

    private bool movingRight = true;   // Direção inicial (começa movendo para a direita)
    private bool isDescending = false; // Controle da descida para não interromper o movimento

    void Update()
    {
        if (!isDescending) MoveEnemies();
    }

    void MoveEnemies()
    {
        // Movimenta o grupo de inimigos horizontalmente
        float horizontalMovement = moveSpeed * Time.deltaTime * (movingRight ? 1 : -1);
        transform.Translate(horizontalMovement, 0, 0);

        // Verifica se o grupo atingiu a borda da tela
        if (movingRight && transform.position.x >= boundaryX)
        {
            StartCoroutine(ChangeDirectionSmooth());
        }
        else if (!movingRight && transform.position.x <= -boundaryX)
        {
            StartCoroutine(ChangeDirectionSmooth());
        }
    }

    IEnumerator ChangeDirectionSmooth()
    {
        isDescending = true;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y - moveDownDistance, startPos.z);
        
        float elapsedTime = 0f;
        while (elapsedTime < descendDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / descendDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        movingRight = !movingRight;
        isDescending = false;
    }
}