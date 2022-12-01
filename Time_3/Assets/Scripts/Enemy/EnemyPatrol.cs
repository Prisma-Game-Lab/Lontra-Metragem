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
    [Tooltip("Distancia em tiles percorrida na vertical. Positivo: cima e Negativo: baixo")]
    public int distY;
    [Tooltip("Distancia em tiles percorrida na horizontal. Positivo: direita e Negativo: esquerda")]
    public int distX;
    public PatrolType patrolType;
    [Tooltip("Tempo que o inimigo fica parado na patrulha com descanso")]
    public float restTime;
    [Tooltip("Tempo entre cada etapa de movimento")]
    public float changeDirectionPause;
    public float speed;
    [Tooltip("Se selecionado: movimento em quadrado")]
    public bool squareMoving;
    [Tooltip("Se selecionado: movimento na vertical eh feito antes da horizontal")]
    public bool verticalFirst;
    public List<Sprite> spriteList;

    private float waitTime;
    private Vector2[] positions;
    private int[] distances;
    private Vector2[] directions;
    private int movementStep = 0;
    private bool waiting;
    private Vector3 startPosition;
    private int i;
    private SpriteRenderer enemySprite;

    void Start()
    {
        startPosition = transform.position;
        positions = new Vector2[4];
        BuildPositionsArray();
        distances = new int[4];
        directions = new Vector2[4];
        BuildDistancesArray();
        BuildDirectionsArray();
        waitTime = changeDirectionPause;
        enemySprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
                MoveWithRest(changeDirectionPause);
                break;

            case PatrolType.paused:
                MoveStepByStep(changeDirectionPause);
                break;
            case PatrolType.withRest:
                if (!waiting)
                {
                    MoveWithRest(changeDirectionPause);
                }
                if (movementStep == 4)
                {
                    waiting = true;
                    StartCoroutine(Wait(restTime));
                }
                break;
        }
        movementStep %= 4;
    }

    private void MoveWithRest(float rest)
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[movementStep], speed * Time.deltaTime);
        enemySprite.sprite = SelectSprite();
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
        var factor = Vector2.zero;
        if(distances[movementStep] != 0)
            factor = directions[movementStep] * (distances[movementStep] / Mathf.Abs(distances[movementStep]))*i;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)startPosition + factor, speed * Time.deltaTime);
        enemySprite.sprite = SelectSprite();
        if (Vector2.Distance(transform.position, (Vector2)startPosition +  factor) < 0.2f)
        {
            if (waitTime <= 0)
            {
                waitTime = rest;
                i++;
                if(i == Mathf.Abs(distances[movementStep])+1)
                {
                    movementStep++;
                    startPosition = transform.position;
                    i = 0;
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
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

    private void BuildDistancesArray()
    {
        if (verticalFirst)
        {
            distances[0] = distY;
            distances[1] = distX;
            distances[2] = -distX;
            distances[3] = -distY;
            if (squareMoving)
            {
                distances[2] = -distY;
                distances[3] = -distX;
            }
                
        }
        else
        {
            distances[0] = distX;
            distances[1] = distY;
            distances[2] = -distY;
            distances[3] = -distX;
            if (squareMoving)
            {
                distances[2] = -distX;
                distances[3] = -distY;
            }     
        }
    }

    private void BuildDirectionsArray()
    {
        if (verticalFirst)
        {
            directions[0] = Vector2.up;
            directions[1] = Vector2.right;
            directions[2] = Vector2.right;
            directions[3] = Vector2.up;
            if (squareMoving)
            {
                directions[2] = Vector2.up;
                directions[3] = Vector2.right;
            }
                
        }
        else
        {
            directions[0] = Vector2.right;
            directions[1] = Vector2.up;
            directions[2] = Vector2.up;
            directions[3] = Vector2.right;
            if (squareMoving)
            {
                directions[2] = Vector2.right;
                directions[3] = Vector2.up;
            }
        }
    }

    private Sprite SelectSprite()
    {
        Vector3 direction = directions[movementStep] * distances[movementStep];
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))//movimento horizontal
        {
            if (direction.x > 0)
            {
                return spriteList[0];
            }
            else
            {
                return spriteList[1];
            }
        }
        else//movimento vertical
        {
            if (direction.y > 0)
            {
                return spriteList[2];
            }
            else
            {
                return spriteList[3];
            }
        }
    }
}

