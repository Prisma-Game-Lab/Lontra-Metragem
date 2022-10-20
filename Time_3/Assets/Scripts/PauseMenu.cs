using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        PauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
