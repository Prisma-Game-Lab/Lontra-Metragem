using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public Slider playerDetectionBar;
    public float detectionRate;
    public float maxDetectionValue;
    private PlayerSlingshotMovement playerSlingshot;
    private PlayerStatus playerStatus;
    private bool inDanger;

    void Start()
    {
        playerSlingshot = GetComponent<PlayerSlingshotMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        playerDetectionBar.maxValue = maxDetectionValue;
        playerDetectionBar.value = 0f;
    }

    private void Update()
    {
        if (inDanger)
            IncreaseDetectionBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Danger")
        {
            inDanger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inDanger = false;
    }

    private IEnumerator StartTimer()
    {
            playerSlingshot.onSlingshot = false;
            playerStatus.fade.GetComponent<Animator>().Play("FadeIn");
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }

    private void IncreaseDetectionBar()
    {
        if( playerDetectionBar.value == maxDetectionValue)
        {
            StartCoroutine(StartTimer());
            return;
        }

        playerDetectionBar.value += detectionRate*Time.deltaTime;
    }
}
