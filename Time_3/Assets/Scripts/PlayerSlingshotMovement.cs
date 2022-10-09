using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlingshotMovement : MonoBehaviour
{
    [SerializeField]
    private float impulseForce = 1.0f;
    [SerializeField]
    private float touchRadius;
    private bool onSlingshot = false;
    private Rigidbody2D rb;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private LineRenderer arrowRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer.positionCount = 2;
        arrowRenderer.positionCount = 2;
    }

    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0)){
            if (CheckTouch(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                onSlingshot = true;
                Time.timeScale = 0.5f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (onSlingshot)
            {
                //Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red);
                var diff = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = new Vector3(diff.x, diff.y, 0f);
                SetDirectionLine(direction);
            }
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
                Vector3[] positions = { Vector3.zero, Vector3.zero};
                lineRenderer.SetPositions(positions);
                arrowRenderer.SetPositions(positions);
                Time.timeScale = 1f;
            }      
        }
    }

    private bool CheckTouch(Vector3 initialPosition)
    {
        if (Vector2.Distance(initialPosition, transform.position) > touchRadius)
            return false;
        return true;
    }

    private void SetDirectionLine(Vector3 direction)
    {
        float intensity = direction.magnitude;//Change to work within a range
        lineRenderer.SetPositions(new Vector3[] {transform.position - direction.normalized * intensity, transform.position + direction.normalized * intensity});
        arrowRenderer.SetPositions(new Vector3[]{ transform.position + direction.normalized * intensity, transform.position + (direction.normalized * intensity) * 1.5f});
    }
}
