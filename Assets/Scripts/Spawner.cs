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

    private SpriteRenderer _enemySpriteRenderer;

    public Enemy enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        timer = 0F;
        next = 2F;
	_enemySpriteRenderer = enemyPrefab.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if( timer >= next ){
            var spriteHeight = _enemySpriteRenderer.sprite.bounds.extents.y;
	    var spriteWidth = _enemySpriteRenderer.sprite.bounds.extents.x;
            Instantiate(enemyPrefab, new Vector3(Random.Range(-width+spriteWidth,width-spriteWidth),height-spriteHeight/2f), Quaternion.identity);
            timer = 0F;
            next = Random.Range(6F,12F);
        }
    }
}
