using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngine : MonoBehaviour,
    IMovementController, IGunController
{
    public Projectile projectilePrefab;
    public Enemy spaceship;
    
    private float timerH;
    private float nextH;
    private float timerV;
    private float nextV;
    private bool right;
    private bool down;

    public void OnEnable()
    {
        timerH = 0F;
        nextH = 2F;
        timerV = 0F;
        nextV = 2F;

        right = Random.Range(0f,1f) > 0.5f;
	down = true;
        spaceship.SetMovementController(this);
        spaceship.SetGunController(this);
    }

    public void Update()
    {
	if(spaceship.nearHorBound()){
	    right = !right;
            timerH = 0F;
            nextH = Random.Range(2F,4.1F);
	}
	else{
            timerH += Time.deltaTime;
            if( timerH >= nextH ){
                right = !right;
                timerH = 0F;
                 nextH = Random.Range(2F,4.1F);
            }
	}

	if(spaceship.nearVerBound()){
	    down = !down;
            timerV = 0F;
            nextV = Random.Range(2F,4.1F);
	}
	else{
            timerV += Time.deltaTime;
            if( timerV >= nextV ){
	        down = !down;
                timerV = 0F;
                nextV = Random.Range(2F,4.1F);
            }
	}

	if (down) {
            spaceship.MoveVertically(-0.1f);
        }
        else{
            spaceship.MoveVertically(0.1f);
        }

        if (right) {
            spaceship.MoveHorizontally(1);
        }
        else{
            spaceship.MoveHorizontally(-1);
        }
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
        Instantiate(projectilePrefab,
            transform.position, Quaternion.identity);
    }
}