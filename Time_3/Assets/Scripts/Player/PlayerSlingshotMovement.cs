using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlingshotMovement : MonoBehaviour
{
    [Tooltip("Fator que multiplica o impulso para determinar a intensidade da força aplicada ao player a cada movimento.")]
    [SerializeField]
    private float impulseForce;

    [Tooltip("Raio da area na qual eh possivel clicar para iniciar o movimento do estilingue.")]
    [SerializeField]
    private float touchRadius;

    [Tooltip("Fator que determina a influencia do atrito com o chao no movimento do player.")]
    [SerializeField]
    private float linearDrag;

    [Tooltip("Intensidade maxima de impulso do movimento.")]
    [SerializeField]
    private float maxDragRadius;

    [Tooltip("Intensidade minima de impulso do movimento.")]
    [SerializeField]
    private float minDragRadius;

    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private LineRenderer arrowRenderer;

    [HideInInspector]
    public bool onSlingshot = false;
    private Rigidbody2D rb;
    void Start()
    {
        if (GetComponent<PlayerStatus>().activeMovement == MovementType.joystick)
            this.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer.positionCount = 2;
        arrowRenderer.positionCount = 2;
        rb.drag = linearDrag;
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
                Vector3 direction = SetMovementDirection();
                DrawLine(direction);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (onSlingshot)
            {
                onSlingshot = false;
                Move();
            }
        }
    }

    private bool CheckTouch(Vector3 initialPosition)
    {
        if (Vector2.Distance(initialPosition, transform.position) > touchRadius)
            return false;

        return true;
    }
    private Vector3 SetMovementDirection()
    {
        var diff = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = new Vector3(diff.x, diff.y, 0f);

        return direction;
    }

    private float SetMovementIntensity(Vector3 direction)
    {
        float intensity = direction.magnitude;
        if (direction.magnitude >= maxDragRadius)
            intensity = maxDragRadius;
        else if (direction.magnitude < minDragRadius)
            intensity = 0f;
        
        return intensity;
    }

    private void DrawLine(Vector3 direction)
    {
        float intensity = SetMovementIntensity(direction);
        var v = direction.normalized * intensity;
        lineRenderer.SetPositions(new Vector3[] {transform.position - v, transform.position + v});
        arrowRenderer.SetPositions(new Vector3[] {transform.position + v, transform.position + v * 1.5f});
    }

    private void Move()
    {
        rb.velocity = Vector3.zero;
        Vector3[] positions = { Vector3.zero, Vector3.zero };
        Vector3 direction = SetMovementDirection();

        float intensity = SetMovementIntensity(direction);

        rb.AddForce(direction.normalized * impulseForce * intensity);
        
        lineRenderer.SetPositions(positions);
        arrowRenderer.SetPositions(positions);

        Time.timeScale = 1f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, touchRadius);
    }
}
