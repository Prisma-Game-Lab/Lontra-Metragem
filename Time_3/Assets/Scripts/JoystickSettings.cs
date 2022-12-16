using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickSettings : MonoBehaviour
{
    public GameObject joystick;
    private bool choosingPosition;
    public List<Transform> transforms;

    private void Start()
    {
        joystick.transform.position = new Vector3(PlayerPrefs.GetFloat("JoystickX"), PlayerPrefs.GetFloat("JoystickY"), PlayerPrefs.GetFloat("JoystickZ"));
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckTouch())
            {
                choosingPosition = true;
            }

        }
        else if (Input.GetMouseButton(0))
        {
            if(choosingPosition)
                SetPosition();
        }

        if (Input.GetMouseButtonUp(0))
        {
            choosingPosition = false;
            SavePosition();
        }
    }
    public void SetJoystickInitialPosition()
    {
        if(PlayerPrefs.GetInt("Joystick") != 1)
        {
            SavePosition(transforms[0].position.x, transforms[0].position.y, transforms[0].position.z);
            PlayerPrefs.SetInt("Joystick", 1);
        }  
    }
    private bool CheckTouch()
    {
        if (Vector2.Distance(Input.mousePosition, joystick.transform.position) > 120.0f)
            return false;

        return true;
    }
    private void SetPosition()
    {
        float x = Mathf.Clamp(Input.mousePosition.x, transforms[0].position.x, transforms[1].position.x);
        float y = Mathf.Clamp(Input.mousePosition.y, transforms[0].position.y, transforms[1].position.y);
        joystick.transform.position = new Vector3(x, y, Input.mousePosition.z);
    }
    public void RestorePosition()
    {
        joystick.transform.position = transforms[0].position;
        SavePosition();
    }
    private void SavePosition()
    {
        PlayerPrefs.SetFloat("JoystickX", joystick.transform.position.x);
        PlayerPrefs.SetFloat("JoystickY", joystick.transform.position.y);
        PlayerPrefs.SetFloat("JoystickZ", joystick.transform.position.z);
    }
    private void SavePosition(float x, float y, float z)
    {
        PlayerPrefs.SetFloat("JoystickX", x);
        PlayerPrefs.SetFloat("JoystickY", y);
        PlayerPrefs.SetFloat("JoystickZ", z);
    }
}
