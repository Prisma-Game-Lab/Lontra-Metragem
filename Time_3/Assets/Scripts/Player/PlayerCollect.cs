using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    private PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dvd")
        {
            playerStatus.hasDVD = true;
            Destroy(collision.gameObject);
        }
    }

}
