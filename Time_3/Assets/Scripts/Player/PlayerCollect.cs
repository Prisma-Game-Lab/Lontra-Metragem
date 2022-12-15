using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
    public GameObject exitIndicator;
    public GameObject dvd;
    public Slider collectLoad;
    public float tempoEspera = 5f;

    private Coroutine lastCoroutine;
    private PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        collectLoad.enabled = false;
        collectLoad.value = 0;
        collectLoad.maxValue = tempoEspera;   
    }

    private void Update()
    {
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
            collectLoad.enabled = true;
            collectLoad.gameObject.SetActive(true);
            collectLoad.transform.position = Camera.main.WorldToScreenPoint(new Vector3(dvd.transform.position.x, dvd.transform.position.y - 0.5f, 0.0f));
            lastCoroutine = StartCoroutine(Esperar(tempoEspera));
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Dvd")
        {
            StopCoroutine(lastCoroutine);
            collectLoad.enabled = false;
            collectLoad.gameObject.SetActive(false);
            collectLoad.value = 0;
        }
    }
    
    IEnumerator Esperar(float tempoEspera)
    {
        yield return new WaitForSeconds (tempoEspera);
        playerStatus.hasDVD = true;
        exitIndicator.SetActive(true);
        Destroy(dvd);
    }
}