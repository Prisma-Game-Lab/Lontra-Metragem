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

    public float tempoEspera = 5f;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        podeColetar = false;
        
    }

    private void Update()
    {
         if(podeColetar == true)
            {
                playerStatus.hasDVD = true;
                if(SceneManager.GetActiveScene().name == "Teste0")
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
            StartCoroutine(Esperar(tempoEspera));
           
        }
    }

    IEnumerator Esperar(float tempoEspera)
    {
        yield return new WaitForSeconds (tempoEspera);
        podeColetar = true;
    }

}