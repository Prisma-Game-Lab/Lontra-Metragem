using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChooseMovementTypeButton : MonoBehaviour
{
    public PauseMenu PauseMenu;
    void Start()
    {
        ChangeText();
        PauseMenu.OnMovementChange += ChangeText;
    }
    public void ChangeText()
    {
        int movement = PlayerPrefs.GetInt("Movement");
        if(movement == (int)MovementType.joystick)
        {
            GetComponent<TextMeshProUGUI>().text = "Mudar para Estilíngue";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Mudar para Joystick";
        }
    }
}
