using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
    private PlayerStatus playerStatus;

    public GameObject exitIndicator;

    public GameObject dvd;

    public Slider collectLoad;

    public bool podeColetar;
    public bool isTriggered;
    

    public float tempoEspera = 5f;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        podeColetar = false;
        collectLoad.enabled = false;
        collectLoad.value = 0;
        collectLoad.maxValue = tempoEspera;
        
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

         if(collectLoad.isActiveAndEnabled)
         {
            collectLoad.value += 1 * Time.deltaTime;
         }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dvd" )
        {  
            dvd = collision.gameObject;
            isTriggered = true;
            collectLoad.enabled = true;
            collectLoad.gameObject.SetActive(true);
            StartCoroutine(Esperar(tempoEspera));
        }  

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Dvd")
        {
            isTriggered = false;
            collectLoad.enabled = false;
            collectLoad.gameObject.SetActive(false);
            collectLoad.value = 0;
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