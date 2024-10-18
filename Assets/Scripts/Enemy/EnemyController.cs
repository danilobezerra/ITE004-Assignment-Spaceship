using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy oriEnemy;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        this.spawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.spawnTime += Time.deltaTime;
        if (this.spawnTime >= 1f)
        {
            this.spawnTime = 0;

            //cria o ponto do spawn na parte superior fora da camera
            Vector2 posicaoMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            Vector2 posicaoMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            float posicaoX = Random.Range(posicaoMin.x, posicaoMax.x);
            Vector2 posicaoEnemy = new Vector2(posicaoX, posicaoMax.y);
            //cria novo inimigo
            Instantiate(this.oriEnemy, posicaoEnemy, Quaternion.identity);
        }
    }
}
