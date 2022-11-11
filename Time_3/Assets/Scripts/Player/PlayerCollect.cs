using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
    private PlayerStatus playerStatus;

    public GameObject exitIndicator;

    public GameObject dvd;

    public bool podeColetar;
    public bool isTriggered;
    

    public float tempoEspera = 5f;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        podeColetar = false;
        
    }

    private void Update()
    {
        if(podeColetar == true && isTriggered == true)
        {
            playerStatus.hasDVD = true;
            exitIndicator.SetActive(true);
            Destroy(dvd);
            podeColetar = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dvd" )
        {  
            dvd = collision.gameObject;
            isTriggered = true;
            StartCoroutine(Esperar(tempoEspera));
        }  

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Dvd")
        {
            isTriggered = false;
            podeColetar = false;
        }
    }

    IEnumerator Esperar(float tempoEspera)
    {
        yield return new WaitForSeconds (tempoEspera);
        if(isTriggered == false)
        {
            podeColetar = false;
        }
        else
        {
            podeColetar = true;
        }
    }

}