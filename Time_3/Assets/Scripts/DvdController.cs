using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DvdController : MonoBehaviour
{
    [SerializeField]
    private int count = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            count++;
            Destroy(gameObject);            
        }
    }
}