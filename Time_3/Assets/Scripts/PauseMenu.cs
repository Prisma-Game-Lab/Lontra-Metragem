using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    [SerializeField]
    private GameObject UIConfig;
    [SerializeField]
    private GameObject joystick;
    [SerializeField]
    private GameObject UIJoystickSettings;
    private int controlOpt;
    private GameObject player;


    private void Awake()
    {
        AudioManager.instance.Stop("MainMenu");
        AudioManager.instance.Stop("InGame");
    }
    private void Start()
    {
        AudioManager.instance.Play("InGame");
    }
    private void OnEnable()
    {
        controlOpt = PlayerPrefs.GetInt("Movement");
        player = GameObject.FindGameObjectWithTag("Player");
        joystick = GameObject.FindGameObjectWithTag("Joystick");
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        PauseUI.SetActive(true);
        Debug.Log(Time.timeScale);
        StopMovement();

    }
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
        EnableMovement();
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenConfig()
    {
        UIConfig.SetActive(true);
    }

    public void ChooseMovement()
    {
        controlOpt++;
        controlOpt %= 2;
        PlayerPrefs.SetInt("Movement", controlOpt);
        player.GetComponent<PlayerStatus>().activeMovement = (MovementType)controlOpt;
        StopMovement();   
    }

    private void StopMovement()
    {
        if (controlOpt == 0)
            player.GetComponent<PlayerSlingshotMovement>().enabled = false;
        else
        {
            player.GetComponent<PlayerJoystickMovement>().enabled = false;
            joystick.SetActive(false);
        }        
    }
    private void EnableMovement()
    {
        if (controlOpt == 0)
            player.GetComponent<PlayerSlingshotMovement>().enabled = true;
        else
            player.GetComponent<PlayerJoystickMovement>().enabled = true;
    }
    public void CloseConfig()
    {
        UIConfig.SetActive(false);
    }

    public void ChangeFont()
    {
        FontManager.instance.ChangeFont();
    }
    public void OpenJoystickSettings()
    {
        UIJoystickSettings.SetActive(true);
        UIConfig.SetActive(false);
    }

    public void CloseJoystickSettings()
    {
        UIJoystickSettings.SetActive(false);
        UIConfig.SetActive(true);
    }
}
