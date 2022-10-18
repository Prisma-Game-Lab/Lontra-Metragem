using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MovementType
{
    slingshot,
    joystick
}
public class PlayerStatus : MonoBehaviour
{
    public GameObject fade;
    [HideInInspector]
    public bool hasDVD;
    public MovementType activeMovement;

    [SerializeField]
    private string nextLevel;
    // Start is called before the first frame update
    void Awake()
    {
        activeMovement = GetActiveMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exit") && hasDVD)
            StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        fade.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nextLevel);
    }

    private MovementType GetActiveMovement()
    {
        var option = PlayerPrefs.GetInt("Movement");
        if (option == 0)
            return MovementType.slingshot;
        else
            return MovementType.joystick;
    }
}
