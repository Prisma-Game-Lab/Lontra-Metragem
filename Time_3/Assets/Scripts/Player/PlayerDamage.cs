using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    [Tooltip("Tempo que o player pode permanecer na area de perigo sem ser afetado.")]
    [SerializeField]
    private float timeLimit;
    private bool inDanger;
    private PlayerSlingshotMovement playerSlingshot;
    private PlayerStatus playerStatus;

    void Start()
    {
        playerSlingshot = GetComponent<PlayerSlingshotMovement>();
        playerStatus = GetComponent<PlayerStatus>();
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
            playerSlingshot.onSlingshot = false;
            playerStatus.fade.GetComponent<Animator>().Play("FadeIn");
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   
    }
}
