using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] //faz com que a variável apareça no inspector mesmo sendo privada
    private GameObject UIConfig;
    [SerializeField]
    private GameObject ChooseMovementUI;
    [SerializeField]
    private GameObject UICredits;
    [SerializeField]
    private GameObject fade;
    private string scene;

    private void Start()
    {
        AudioManager.instance.Play("Teste");
    }
    public void PlayGame(string scene)
    {
        ChooseMovementUI.SetActive(true);
        this.scene = scene;
    }

    public void OpenConfig()
    {
        UIConfig.SetActive(true);
        UICredits.SetActive(false);
    }

    public void CloseConfig()
    {
        UIConfig.SetActive(false);
    }

    public void OpenCredits()
    {
        UICredits.SetActive(true);
        UIConfig.SetActive(false);
    }
    public void CloseCredits()
    {
        UICredits.SetActive(false);
    }

    public void ChooseMovement(string name)
    {
        if(name == "slingshot")
            PlayerPrefs.SetInt("Movement", ((int)MovementType.slingshot));
        else if(name == "joystick")
            PlayerPrefs.SetInt("Movement", ((int)MovementType.joystick));
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        fade.GetComponent<Animator>().Play("FadeIn");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
    public void ChangeFont()
    {
        FontManager.instance.ChangeFont();
    }
}
