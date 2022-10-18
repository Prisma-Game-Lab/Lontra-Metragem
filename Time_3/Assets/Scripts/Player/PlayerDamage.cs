using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Tooltip("Tempo que o player pode permanecer na area de perigo sem ser afetado.")]
    [SerializeField]
    private float timeLimit;

    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private bool inDanger;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Danger")
            StartCoroutine(StartTimer());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Danger")
        {
            inDanger = false;
            StopCoroutine(StartTimer());
        }
    }

    private IEnumerator StartTimer()
    {
        inDanger = true;
        yield return new WaitForSeconds(timeLimit);
        if (inDanger)
        {
            transform.position = initialPosition;
            rb.velocity = Vector3.zero;
        }   
    }
}
