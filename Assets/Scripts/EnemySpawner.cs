using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public float waitTime = 3;
    private float currentTime;
    void Update()
    {
        if (currentTime > waitTime)
        {
            Spawnar();
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
    }

    public void Spawnar()
    {
        Instantiate(enemyPrefab, new Vector3(Random.Range(-8f, 8f), 7, 0), Quaternion.identity);
    }
}
