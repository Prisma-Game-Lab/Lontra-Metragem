using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PatrolType
{
    nonStop,
    paused,
    withRest
}

public class EnemyPatrol : MonoBehaviour
{
    public int distY;
    public int distX;
    public PatrolType patrolType;
    [Tooltip("Tempo que o inimigo fica parado na patrulha com descanso")]
    public float pauseTime;
    [Tooltip("Tempo entre cada etapa de movimento")]
    public float restTime;
    public float speed;
    public bool squareMoving;
    public bool verticalFirst;

    private float waitTime;
    private Vector2[] positions;
    private int movementStep = 0;
    private bool waiting;

    void Start()
    {
        positions = new Vector2[4];
        BuildPositionsArray();
        waitTime = restTime;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        switch (patrolType)
        {
            case PatrolType.nonStop:
                MoveWithRest(restTime);
                break;

            case PatrolType.paused:
                //MoveStepByStep(0.2f);
                break;
            case PatrolType.withRest:
                if (!waiting)
                {
                    MoveWithRest(restTime);
                }
                if (movementStep == 4)
                {
                    waiting = true;
                    StartCoroutine(Wait(pauseTime));
                }
                break;
        }
        movementStep %= 4;
    }

    private void MoveWithRest(float rest)
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[movementStep], speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, positions[movementStep]) < 0.2f)
        {
            if (waitTime <= 0)
            {
                waitTime = rest;
                movementStep++;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void MoveStepByStep(float rest)
    {
        //falta fazer
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        waiting = false;
    }

    private void BuildPositionsArray()
    {
        if (verticalFirst)
        {
            positions[0] = transform.position + Vector3.up * distY;
            positions[1] = positions[0] + Vector2.right * distX;
            positions[2] = positions[0];
            if (squareMoving)
                positions[2] = positions[1] + Vector2.up * -distY;   
        }
        else
        {
            positions[0] = transform.position + Vector3.right * distX;
            positions[1] = positions[0] + Vector2.up * distY;
            positions[2] = positions[0];
            if (squareMoving)
                positions[2] = positions[1] + Vector2.right * -distX;
        }
        positions[3] = transform.position;
    }
}

