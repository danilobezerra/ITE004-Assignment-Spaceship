using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    public Sprite[] alternativeSprites;
    public float spriteChangeChance = 0.3f; // Chance de mudar o sprite (0.3 = 30%)
    
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSpriteRandomly();
    }

    // Função para substituir o sprite de forma aleatória
    void ChangeSpriteRandomly()
    {
        // Verifica se há sprites alternativos e faz um sorteio para mudar o sprite
        if (alternativeSprites.Length > 0 && Random.value <= spriteChangeChance)
        {
            // Escolhe um sprite aleatório da lista de alternativos
            Sprite newSprite = alternativeSprites[Random.Range(0, alternativeSprites.Length)];
            spriteRenderer.sprite = newSprite; 
        }
    }
}