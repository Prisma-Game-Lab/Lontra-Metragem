using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlingshotMovement : MonoBehaviour
{
    [SerializeField]
    private float impulseForce = 1.0f;
    [SerializeField]
    private float touchRadius = 0.3f;
    private bool onSlingshot = false;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0)){
            if (CheckTouch(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                onSlingshot = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (onSlingshot)
                Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (onSlingshot)
            {
                onSlingshot = false;
                rb.velocity = Vector3.zero;
                var diff = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = new Vector3(diff.x, diff.y, 0f);
                rb.AddForce(direction.normalized * impulseForce);
            }      
        }
    }

    private bool CheckTouch(Vector3 initialPosition)
    {
        if (Mathf.Pow(initialPosition.x - transform.position.x, 2) + Mathf.Pow(initialPosition.y - transform.position.y, 2) > Mathf.Pow(touchRadius, 2))
        {
            return false;
        }
        return true;
    }
}
