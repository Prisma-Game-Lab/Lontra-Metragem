using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //playgame, OpenConfig, OpenCredits, CloseCredits

    [SerializeField] //faz com que a variável apareça no inspector mesmo sendo privada
    private GameObject UIConfig;

    [SerializeField]
    private GameObject UICredits;

    public void PlayGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenConfig()
    {
        UIConfig.SetActive(true);
    }

    public void CloseConfig()
    {
        UIConfig.SetActive(false);
    }

    public void OpenCredits()
    {
        UICredits.SetActive(true);
    }
    public void CloseCredits()
    {
        UICredits.SetActive(false);
    }
}
