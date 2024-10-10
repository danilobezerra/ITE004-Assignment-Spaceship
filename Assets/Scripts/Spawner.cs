using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer;
    private float next;
    private Bounds _cameraBounds;
    private float height;
    private float width;

    public Enemy enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize * 2f;
        width = height * Camera.main.aspect/2f;
        timer = 0F;
        next = 2F;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if( timer >= next ){
            Debug.Log( " I'm alive");
            Instantiate(enemyPrefab, new Vector3(Random.Range(-width,width),height), Quaternion.identity);
            timer = 0F;
            next = Random.Range(6F,12F);
        }
    }
}
