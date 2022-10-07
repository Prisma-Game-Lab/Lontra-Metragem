using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField]
    private int count;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dvd")
        {
            count++;
            Destroy(collision.gameObject);
        }
    }

}
