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
    private Rigidbody2D rb;
    public int distY;
    public int distX;
    public PatrolType patrolType;
    public float speed;
    private Vector2 direction;
    public float timeRest;
    private int distXR;
    private int distYR;
    // Start is called before the first frame update   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        distXR = distX;
        distYR = distY;
    }

    
    void Update()
    {
        Move();
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y);
    }
    
    private void Move()
    {
        switch (patrolType)
        {
            case PatrolType.nonStop:
                SetDirectionX();
                rb.MovePosition(rb.position + direction * distX * speed * Time.fixedDeltaTime);
                StartCoroutine(Rest(0.2f));
                /*
                distX = -distX;
                distY = -distY;

                SetDirectionY();
                rb.MovePosition(rb.position + direction * -distY * stats.speed * Time.fixedDeltaTime);
                SetDirectionX();
                rb.MovePosition(rb.position + direction * -distX * stats.speed * Time.fixedDeltaTime);
                */
                break;

            case PatrolType.paused:
                SetDirectionX();
                rb.MovePosition(rb.position + direction * distX * speed * Time.fixedDeltaTime);
                SetDirectionY();
                rb.MovePosition(rb.position + direction * distY * speed * Time.fixedDeltaTime);
                break;

            case PatrolType.withRest:
                SetDirectionX();
                rb.MovePosition(rb.position + direction * distX * speed * Time.fixedDeltaTime);
                StartCoroutine(Rest(timeRest));
                break;
        }

    }
    private void SetDirectionX()
    {
        if (distX > 0)
        {
            direction = Vector2.right;
        }
        else if (distX < 0)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.zero;
        }
    }


    private void SetDirectionY()
    {
        if (distY > 0)
        {
            direction = Vector2.up;
        }
        else if (distY < 0)
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    private IEnumerator Rest(float time)
    {
        yield return new WaitForSeconds(time); //apenas um intervalo, um retorno falso
        SetDirectionY();
        rb.MovePosition(rb.position + direction * distY * speed * Time.fixedDeltaTime);
    }
}
    
