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
    private GameObject UIJoystickSettings;
    [SerializeField]
    private GameObject fade;
    private string scene;

    private void Awake()
    {
        AudioManager.instance.Stop("InGame");
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
            return;
        UIJoystickSettings.GetComponent<JoystickSettings>().SetJoystickInitialPosition();
        AudioManager.instance.Play("MainMenu");
    }

    public void PlayGame()
    {
        ChooseMovementUI.SetActive(true);
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

    public void OpenJoystickSettings()
    {
        UIJoystickSettings.SetActive(true);
    }

    public void CloseJoystickSettings()
    {
        UIJoystickSettings.SetActive(false);
    }

    public void NextScene(string scene)
    {
        this.scene = scene;
        StartCoroutine(ChangeScene());
    }

    public void SelectGameMode(int num)
    {
        PlayerPrefs.SetInt("GameMode", num);
    }

    public void StartGameMode()
    {
        int mode = PlayerPrefs.GetInt("GameMode");
        switch (mode)
        {
            case 0:
                NextScene("Fase1T");
                break;
            case 1:
                NextScene("Fase1_1");
                break;
            case 2:
                NextScene("Fase1_2");
                break;
            case 3:
                NextScene("Fase1_3");
                break;
        }  
    }
}
