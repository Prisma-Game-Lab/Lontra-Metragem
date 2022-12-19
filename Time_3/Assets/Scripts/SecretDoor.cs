using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretDoor : MonoBehaviour
{
    public string scene;
    public GameObject player;
    private PlayerSlingshotMovement playerSlingshot;
    private PlayerStatus playerStatus;
    private void Start()
    {
        playerSlingshot = player.GetComponent<PlayerSlingshotMovement>();
        playerStatus = player.GetComponent<PlayerStatus>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(ChangeScene());
        }
    }
    private IEnumerator ChangeScene()
    {
        playerSlingshot.onSlingshot = false;
        playerStatus.fade.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
}
