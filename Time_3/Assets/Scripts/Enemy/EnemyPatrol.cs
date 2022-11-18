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
    private bool moving;
    // Start is called before the first frame update   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        distXR = distX;
        distYR = distY;
        //Move();
    }


    void Update()
    {
        //Move();
        if (!moving)
            Move();
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y);
    }

    private void Move()
    {

        switch (patrolType)
        {
            case PatrolType.nonStop:
                //SetDirectionX();
                // rb.MovePosition(rb.position + direction * distX * speed * Time.fixedDeltaTime);
                Debug.Log(direction);
                StartCoroutine(Rest(0.6f));

                break;

            case PatrolType.paused:
                for (int i =  0; i < distX; i++)
                {
                    //SetDirectionX();
                    rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
                }
                for(int i = 0; i < distY; i++)
                {
                    //SetDirectionY();
                    rb.MovePosition(rb.position + Vector2.up * distY * speed * Time.fixedDeltaTime);
                }
                break;

            case PatrolType.withRest:
                SetDirectionX();
                rb.MovePosition(rb.position + direction * distX * speed * Time.fixedDeltaTime);
                StartCoroutine(StopF(timeRest));
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
        moving = true;
        //SetDirectionX();
        rb.MovePosition(rb.position + Vector2.right * distX * speed * Time.fixedDeltaTime);
        Debug.Log("Para direita");
        Debug.Log(direction);
        Debug.Log(distX);
        yield return new WaitForSeconds(time); //apenas um intervalo, um retorno falso
        //SetDirectionY();
        rb.MovePosition(rb.position + Vector2.up * distY * speed * Time.fixedDeltaTime);
        Debug.Log("Para cima");
        Debug.Log(direction);
        Debug.Log(distY);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //SetDirectionY();
        rb.MovePosition(rb.position + Vector2.up * -distY * speed * Time.fixedDeltaTime);
        Debug.Log("Para baixo");
        Debug.Log(direction);
        Debug.Log(-distY);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //SetDirectionX();
        rb.MovePosition(rb.position + Vector2.right * -distX * speed * Time.fixedDeltaTime);
        Debug.Log("Para esquerda");
        Debug.Log(direction);
        Debug.Log(-distX);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //StartCoroutine(Return(0.5f));
        moving = false;
    }

    private IEnumerator Return(float time)
    {
        yield return new WaitForSeconds(time);
        SetDirectionY();
        rb.MovePosition(rb.position + direction * -distY * speed * Time.fixedDeltaTime);
        Debug.Log(direction);
        yield return new WaitForSeconds(time);
        SetDirectionX();
        rb.MovePosition(rb.position + direction * -distX * speed * Time.fixedDeltaTime);
        Debug.Log(direction);
        yield return new WaitForSeconds(time);
    }
    
    private IEnumerator StopF (float time)
    {
         moving = true;
        //SetDirectionX();
        for (int i = 0; i < distX; i++)
        {
            rb.MovePosition(rb.position + Vector2.right * speed * Time.fixedDeltaTime);
        }
            Debug.Log("Para direita");
            Debug.Log(direction);
            Debug.Log(distX);
            
        yield return new WaitForSeconds(time); //apenas um intervalo, um retorno falso
                                               //SetDirectionY();
        for (int i = 0; i < distY; i++)
        {
            rb.MovePosition(rb.position + Vector2.up * speed * Time.fixedDeltaTime);
        }
        Debug.Log("Para cima");
        Debug.Log(direction);
        Debug.Log(distY);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //SetDirectionY();
        for (int i = 0; i < distX; i++)
        {
            rb.MovePosition(rb.position + Vector2.down * speed * Time.fixedDeltaTime);
        }
        Debug.Log("Para baixo");
        Debug.Log(direction);
        Debug.Log(-distY);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //SetDirectionX();
        for (int i = 0; i < distX; i++)
        {
            rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);
        }
        Debug.Log("Para esquerda");
        Debug.Log(direction);
        Debug.Log(-distX);
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        //StartCoroutine(Return(0.5f));
        moving = false;
    }
    
}

