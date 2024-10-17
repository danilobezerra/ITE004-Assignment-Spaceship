using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
    void Move(Transform enemyTransform);
}

public class PatrolMovement : MonoBehaviour, IEnemyMovement
{
    public float speed = 2f;
    public float distance = 5f;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool movingTowardsEnd = true;

    private void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + new Vector3(distance, 0, 0);
    }

    public void Move(Transform enemyTransform)
    {
        if (movingTowardsEnd)
        {
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, endPoint, speed * Time.deltaTime);
            if (Vector3.Distance(enemyTransform.position, endPoint) < 0.1f)
            {
                movingTowardsEnd = false;
            }
        }
        else
        {
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, startPoint, speed * Time.deltaTime);
            if (Vector3.Distance(enemyTransform.position, startPoint) < 0.1f)
            {
                movingTowardsEnd = true;
            }
        }
    }
}

public class Enemy : MonoBehaviour
{
    private IEnemyMovement movementBehavior;

    private void Start()
    {
        movementBehavior = GetComponent<IEnemyMovement>();
        if (movementBehavior == null)
        {
            movementBehavior = gameObject.AddComponent<PatrolMovement>(); // Adiciona PatrolMovement se não estiver presente
            Debug.Log("Patrol movement behavior assigned on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (movementBehavior != null)
        {
            movementBehavior.Move(transform);
        }
    }
}
